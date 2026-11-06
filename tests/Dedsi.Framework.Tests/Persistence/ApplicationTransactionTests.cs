using Dedsi.Ddd.Application;
using Dedsi.Ddd.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Dedsi.Framework.Tests.Persistence;

[Trait("Category", "Integration")]
public class ApplicationTransactionTests : IAsyncLifetime
{
    private IAbpApplicationWithInternalServiceProvider _app = null!;
    private IServiceScope _scope = null!;
    private IUnitOfWorkManager _manager = null!;
    private TransactionalAppService _service = null!;

    public async Task InitializeAsync()
    {
        _app = await AbpApplicationFactory.CreateAsync<TransactionTestModule>(options => options.UseAutofac());
        await _app.InitializeAsync();
        _scope = _app.ServiceProvider.CreateScope();
        _manager = _scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        _service = _scope.ServiceProvider.GetRequiredService<TransactionalAppService>();
        using var uow = _manager.Begin(requiresNew: true, isTransactional: false);
        var db = await _scope.ServiceProvider.GetRequiredService<IDbContextProvider<TestDbContext>>().GetDbContextAsync();
        await db.Database.EnsureCreatedAsync();
        await uow.CompleteAsync();
    }

    public async Task DisposeAsync()
    {
        _scope?.Dispose();
        if (_app != null) { await _app.ShutdownAsync(); _app.Dispose(); }
    }

    private async Task<string[]> ReadPersistedNamesAsync()
    {
        using var scope = _app.ServiceProvider.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = manager.Begin(requiresNew: true, isTransactional: false);
        var db = await scope.ServiceProvider.GetRequiredService<IDbContextProvider<TestDbContext>>().GetDbContextAsync();
        return await db.Items.OrderBy(x => x.Name).Select(x => x.Name).ToArrayAsync();
    }

    [Fact]
    public async Task Application_service_automatically_commits_all_writes()
    {
        Assert.Null(_manager.Current);
        await _service.CreatePairAsync(fail: false);
        Assert.Null(_manager.Current);
        Assert.Equal(new[] { "First", "Second" }, await ReadPersistedNamesAsync());
    }

    [Fact]
    public async Task Application_service_rolls_back_even_already_flushed_writes_on_failure()
    {
        Assert.Null(_manager.Current);
        var error = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreatePairAsync(fail: true));
        Assert.Equal("Simulated failure after both database writes.", error.Message);
        Assert.Null(_manager.Current);
        Assert.Empty(await ReadPersistedNamesAsync());
    }

    [Fact]
    public async Task Application_service_joins_outer_unit_of_work_without_committing_it()
    {
        using (var outer = _manager.Begin(requiresNew: true, isTransactional: true))
        {
            var executionUnitOfWork = await _service.CreatePairAsync(fail: false);
            Assert.Equal(outer.Id, executionUnitOfWork);
            Assert.False(outer.IsCompleted);
            // Deliberately do not CompleteAsync: outer disposal must roll back both inserts.
        }
        Assert.Empty(await ReadPersistedNamesAsync());
    }
}

public class TransactionalAppService(TestRepository repository, IUnitOfWorkManager manager) : DedsiApplicationService
{
    public virtual async Task<Guid> CreatePairAsync(bool fail)
    {
        var unitOfWork = manager.Current ?? throw new InvalidOperationException("Application service did not start a unit of work.");
        if (!unitOfWork.Options.IsTransactional)
            throw new InvalidOperationException("Write application service must be transactional.");
        await repository.InsertAsync(new TestItem("First", 1), autoSave: true);
        await repository.InsertAsync(new TestItem("Second", 2), autoSave: true);
        if (fail) throw new InvalidOperationException("Simulated failure after both database writes.");
        return unitOfWork.Id;
    }
}

[DependsOn(typeof(RepositoryTestModule), typeof(DedsiDddApplicationModule))]
public class TransactionTestModule : AbpModule
{
    public TransactionTestModule() => SkipAutoServiceRegistration = true;
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<TransactionalAppService>();
    }
}
