using Dedsi.EntityFrameworkCore;
using Dedsi.EntityFrameworkCore.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Security.Claims;
using System.Security.Claims;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Dedsi.Framework.Tests.Persistence;

[Trait("Category", "Integration")]
public class RepositoryTests : IAsyncLifetime
{
    private IAbpApplicationWithInternalServiceProvider _app = null!;
    private IServiceScope _scope = null!;
    private TestDbContext _db = null!;
    private TestRepository _repository = null!;

    public async Task InitializeAsync()
    {
        _app = await AbpApplicationFactory.CreateAsync<RepositoryTestModule>(options => options.UseAutofac());
        await _app.InitializeAsync();
        _scope = _app.ServiceProvider.CreateScope();
        _repository = _scope.ServiceProvider.GetRequiredService<TestRepository>();
    }

    private IUnitOfWork Begin() => _scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Begin(requiresNew: true, isTransactional: true);

    private async Task SeedAsync()
    {
        _db = await _scope.ServiceProvider.GetRequiredService<IDbContextProvider<TestDbContext>>().GetDbContextAsync();
        await _db.Database.EnsureCreatedAsync();
        await _db.Items.AddRangeAsync(
            new TestItem("A", 10), new TestItem("B", 20), new TestItem("C", 30), new TestItem("D", 40));
        await _db.SaveChangesAsync();
        _db.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        _scope?.Dispose();
        if (_app != null) { await _app.ShutdownAsync(); _app.Dispose(); }
    }

    [Theory]
    [InlineData(false, "B")]
    [InlineData(true, "C")]
    public async Task Pagination_filters_counts_and_orders_before_slicing(bool descending, string expected)
    {
        using var uow = Begin();
        await SeedAsync();
        var (total, rows) = await _repository.GetPagedListAsync(x => x.Score >= 20, x => x.Score, descending, 1, 2);
        Assert.Equal(3, total);
        Assert.Equal(2, rows.Count);
        Assert.Equal(expected, descending ? rows[1].Name : rows[0].Name);
        Assert.Equal(descending ? new[] { 40, 30 } : new[] { 20, 30 }, rows.Select(x => x.Score));
    }

    [Fact]
    public async Task Later_and_empty_pages_preserve_total_count()
    {
        using var uow = Begin();
        await SeedAsync();
        var (total, rows) = await _repository.GetPagedListAsync(x => true, x => x.Score, false, 2, 3);
        Assert.Equal(4, total);
        Assert.Equal("D", Assert.Single(rows).Name);
        var (emptyTotal, emptyRows) = await _repository.GetPagedListAsync(x => true, x => x.Score, false, 3, 3);
        Assert.Equal(4, emptyTotal);
        Assert.Empty(emptyRows);
        var (noMatches, none) = await _repository.GetPagedListAsync(x => x.Score < 0, x => x.Score);
        Assert.Equal(0, noMatches);
        Assert.Empty(none);
    }

    [Theory]
    [InlineData(0, 10, "pageIndex")]
    [InlineData(-1, 10, "pageIndex")]
    [InlineData(1, 0, "pageSize")]
    [InlineData(1, -1, "pageSize")]
    [InlineData(1, 1001, "pageSize")]
    [InlineData(int.MaxValue, 10, "pageIndex")]
    [InlineData(214748366, 10, "pageIndex")]
    public async Task Invalid_paging_is_rejected_before_database_access(int page, int size, string parameter)
    {
        // No schema is created: validation must run before count/query execution.
        using var uow = Begin();
        var error = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _repository.GetPagedListAsync(x => true, x => x.Score, pageIndex: page, pageSize: size));
        Assert.Equal(parameter, error.ParamName);
    }

    [Theory]
    [InlineData(1, 1000, 4)]
    [InlineData(int.MaxValue, 1, 0)]
    public async Task Valid_paging_boundaries_execute_against_sqlite(int page, int size, int expectedRows)
    {
        using var uow = Begin();
        await SeedAsync();
        var (total, rows) = await _repository.GetPagedListAsync(x => true, x => x.Score, pageIndex: page, pageSize: size);
        Assert.Equal(4, total);
        Assert.Equal(expectedRows, rows.Count);
    }

    [Fact]
    public async Task No_tracking_query_leaves_change_tracker_empty()
    {
        using var uow = Begin();
        await SeedAsync();
        var rows = await (await _repository.GetQueryableNoTrackingAsync()).ToListAsync();
        Assert.Equal(4, rows.Count);
        Assert.Empty(_db.ChangeTracker.Entries<TestItem>());
    }

    [Fact]
    public async Task Query_preserves_abp_soft_delete_filter()
    {
        using var uow = Begin();
        await SeedAsync();
        var entity = await _db.Items.SingleAsync(x => x.Name == "A");
        await _repository.DeleteAsync(entity, autoSave: true);
        _db.ChangeTracker.Clear();
        Assert.Equal(3, await (await _repository.GetQueryableNoTrackingAsync()).CountAsync());
        Assert.Equal(4, await _db.Items.IgnoreQueryFilters().CountAsync());
        var (total, rows) = await _repository.GetPagedListAsync(x => true, x => x.Score);
        Assert.Equal(3, total);
        Assert.DoesNotContain(rows, x => x.Name == "A");
    }

    [Fact]
    public async Task Interpolated_sql_treats_input_as_data()
    {
        using var uow = Begin();
        await SeedAsync();
        var input = "Robert'; DROP TABLE Items;--";
        Assert.Equal(1, await _repository.ExecuteSqlAsync($"UPDATE Items SET Name = {input} WHERE Score = {10}"));
        Assert.Equal(4, await _db.Items.CountAsync());
        Assert.Equal(input, (await _db.Items.SingleAsync(x => x.Score == 10)).Name);
    }

    [Fact]
    public async Task Cancelled_query_propagates_cancellation()
    {
        using var uow = Begin();
        await SeedAsync();
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => _repository.GetPagedListAsync(x => true, x => x.Score, cancellationToken: cts.Token));
    }

    [Fact]
    public async Task Bulk_delete_should_preserve_soft_deleted_rows()
    {
        using var uow = Begin();
        await SeedAsync();
        Assert.Equal(2, await _repository.DeleteManyAsync(x => x.Score <= 20, autoSave: true));
        _db.ChangeTracker.Clear();
        Assert.Equal(2, await _db.Items.CountAsync());
        Assert.Equal(4, await _db.Items.IgnoreQueryFilters().CountAsync());
        Assert.Equal(2, await _db.Items.IgnoreQueryFilters().CountAsync(x => x.IsDeleted));
    }
    [Fact]
    public async Task Normal_bulk_delete_defers_save_and_populates_deletion_audit_and_events()
    {
        using var uow = Begin();
        await SeedAsync();
        var actorId = Guid.NewGuid();
        using var principal = _scope.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>().Change(
            new ClaimsPrincipal(new ClaimsIdentity([new Claim(AbpClaimTypes.UserId, actorId.ToString())], "test")));
        var deleted = new List<Guid>();
        using var subscription = _scope.ServiceProvider.GetRequiredService<ILocalEventBus>()
            .Subscribe<EntityDeletedEventData<TestItem>>(evt => { deleted.Add(evt.Entity.Id); return Task.CompletedTask; });
        Assert.Equal(2, await _repository.DeleteManyAsync(x => x.Score <= 20));
        Assert.Equal(0, await _db.Items.IgnoreQueryFilters().AsNoTracking().CountAsync(x => x.IsDeleted));
        Assert.Empty(deleted);
        await uow.CompleteAsync();
        var rows = await _db.Items.IgnoreQueryFilters().AsNoTracking().Where(x => x.IsDeleted).ToArrayAsync();
        Assert.Equal(2, rows.Length);
        Assert.All(rows, row => { Assert.NotNull(row.DeletionTime); Assert.Equal(actorId, row.DeleterId); });
        Assert.Equal(rows.Select(x => x.Id).OrderBy(x => x), deleted.OrderBy(x => x));
    }

    [Fact]
    public async Task Direct_delete_is_physical_and_does_not_save_unrelated_tracked_changes_or_emit_events()
    {
        using var uow = Begin();
        await SeedAsync();
        var deleted = new List<Guid>();
        using var subscription = _scope.ServiceProvider.GetRequiredService<ILocalEventBus>()
            .Subscribe<EntityDeletedEventData<TestItem>>(evt => { deleted.Add(evt.Entity.Id); return Task.CompletedTask; });
        var other = await _db.Items.SingleAsync(x => x.Name == "B");
        other.Score = 999;
        Assert.Equal(1, await _repository.DeleteDirectWithCountAsync(x => x.Name == "A"));
        Assert.Equal(3, await _db.Items.IgnoreQueryFilters().CountAsync());
        Assert.Equal(20, await _db.Items.AsNoTracking().Where(x => x.Name == "B").Select(x => x.Score).SingleAsync());
        // Remove the unrelated pending change before completing to isolate direct deletion behavior.
        _db.ChangeTracker.Clear();
        await uow.CompleteAsync();
        Assert.Empty(deleted);
    }

    [Fact]
    public async Task Direct_delete_preserves_soft_delete_query_filter()
    {
        using var uow = Begin();
        await SeedAsync();
        await _repository.DeleteManyAsync(x => x.Name == "A", autoSave: true);
        _db.ChangeTracker.Clear();
        Assert.Equal(3, await _repository.DeleteDirectWithCountAsync(x => true));
        var remaining = Assert.Single(await _db.Items.IgnoreQueryFilters().ToArrayAsync());
        Assert.Equal("A", remaining.Name);
        Assert.True(remaining.IsDeleted);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Both_delete_modes_roll_back_with_outer_transaction(bool direct)
    {
        using (var seed = Begin())
        {
            await SeedAsync();
            await seed.CompleteAsync();
        }
        using (var deleting = Begin())
        {
            var count = direct
                ? await _repository.DeleteDirectWithCountAsync(x => x.Score <= 20)
                : await _repository.DeleteManyAsync(x => x.Score <= 20, autoSave: true);
            Assert.Equal(2, count);
            Assert.Equal(2, await _repository.GetCountAsync());
            // No CompleteAsync: the delete must be rolled back.
        }
        using var verification = Begin();
        var db = await _scope.ServiceProvider.GetRequiredService<IDbContextProvider<TestDbContext>>().GetDbContextAsync();
        Assert.Equal(4, await db.Items.CountAsync());
        Assert.Equal(0, await db.Items.IgnoreQueryFilters().CountAsync(x => x.IsDeleted));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Delete_modes_report_no_matches_and_honor_cancellation(bool direct)
    {
        using var uow = Begin();
        await SeedAsync();
        Assert.Equal(0, direct
            ? await _repository.DeleteDirectWithCountAsync(x => x.Score < 0)
            : await _repository.DeleteManyAsync(x => x.Score < 0, autoSave: true));
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => direct
            ? _repository.DeleteDirectWithCountAsync(x => true, cts.Token)
            : _repository.DeleteManyAsync(x => true, cancellationToken: cts.Token));
        Assert.Equal(4, await _db.Items.CountAsync());
    }

}

public class TestItem : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = "";
    public int Score { get; set; }
    protected TestItem() { }
    public TestItem(string name, int score) : base(Guid.NewGuid()) { Name = name; Score = score; }
}

public class TestDbContext(DbContextOptions<TestDbContext> options) : DedsiEfCoreDbContext<TestDbContext>(options)
{
    public DbSet<TestItem> Items => Set<TestItem>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TestItem>(b => { b.ToTable("Items"); b.ConfigureByConvention(); });
    }
}

public class TestRepository(IDbContextProvider<TestDbContext> provider) : DedsiEfCoreRepository<TestDbContext, TestItem, Guid>(provider);

[DependsOn(typeof(AbpAutofacModule), typeof(AbpEntityFrameworkCoreSqliteModule), typeof(DedsiEntityFrameworkCoreModule))]
public class RepositoryTestModule : AbpModule
{
    public RepositoryTestModule() => SkipAutoServiceRegistration = true;
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(_ => { var connection = new SqliteConnection("Data Source=:memory:"); connection.Open(); return connection; });
        context.Services.AddAbpDbContext<TestDbContext>();
        context.Services.AddTransient<TestDbContext>();
        context.Services.AddTransient<TestRepository>();
        Configure<AbpDbContextOptions>(options => options.Configure<TestDbContext>(c => c.DbContextOptions.UseSqlite(c.ServiceProvider.GetRequiredService<SqliteConnection>())));
    }
}
