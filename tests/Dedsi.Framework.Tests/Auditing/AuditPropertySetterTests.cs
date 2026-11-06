using Dedsi.Ddd.Domain.Entities;
using Dedsi.Ddd.Domain.Auditing.Contracts;
using Dedsi.EntityFrameworkCore.PropertySetters;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Dedsi.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Dedsi.Framework.Tests.Auditing;

public class AuditPropertySetterTests
{
    private static readonly DateTime Now = new(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc);
    private static DedsiAuditPropertySetter Create(Guid? id, string? name, string? userName = null)
    {
        var user = new Mock<ICurrentUser>();
        user.SetupGet(x => x.Id).Returns(id);
        user.SetupGet(x => x.Name).Returns(name);
        user.SetupGet(x => x.UserName).Returns(userName);
        var clock = new Mock<IClock>();
        clock.SetupGet(x => x.Now).Returns(Now);
        return new DedsiAuditPropertySetter(user.Object, Mock.Of<ICurrentTenant>(), clock.Object);
    }

    public class Order : DedsiAggregateRoot<Guid>
    {
        public void SetAudit(Guid id, string name, DateTime time) { CreatorId = id; CreatorName = name; CreationTime = time; }
    }
    public class AbpOrder : CreationAuditedAggregateRoot<Guid>;

    public class PrivateSetterEntity(DateTime creationTime) : IDedsiCreationAuditedObject
    {
        public Guid CreatorId { get; private set; }
        public string CreatorName { get; private set; } = "";
        public DateTime CreationTime { get; private set; } = creationTime;
    }

    [Fact]
    public void Audit_populates_creator_and_uses_injected_clock()
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        Create(id, "Alice").SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal("Alice", entity.CreatorName);
        Assert.Equal(Now, entity.CreationTime);
    }

    [Fact]
    public void Explicit_audit_values_are_preserved_without_logged_in_user()
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        var time = Now.AddDays(-1);
        entity.SetAudit(id, "Importer", time);
        Create(null, null).SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal("Importer", entity.CreatorName);
        Assert.Equal(time, entity.CreationTime);
    }

    [Fact]
    public void Audit_supports_private_setters_and_preserves_creation_time()
    {
        var id = Guid.NewGuid();
        var entity = new PrivateSetterEntity(Now.AddDays(-1));
        var time = entity.CreationTime;
        Create(id, "Alice").SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal("Alice", entity.CreatorName);
        Assert.Equal(time, entity.CreationTime);
    }

    [Fact]
    public void Standard_abp_audit_still_runs()
    {
        var id = Guid.NewGuid();
        var entity = new AbpOrder();
        Create(id, "Alice").SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal(Now, entity.CreationTime);
        Create(id, "Alice").SetCreationProperties(new object());
    }

    [Fact]
    public void Anonymous_creation_without_explicit_creator_is_rejected_without_partial_changes()
    {
        var entity = new Order();
        Assert.Throws<ArgumentException>(() => Create(null, null).SetCreationProperties(entity));
        Assert.Equal(Guid.Empty, entity.CreatorId);
        Assert.Null(entity.CreatorName);
        Assert.Equal(default, entity.CreationTime);
    }

    [Fact]
    public void Empty_current_user_id_is_rejected()
        => Assert.Throws<ArgumentException>(() => Create(Guid.Empty, "System").SetCreationProperties(new Order()));

    [Theory]
    [InlineData(null, "alice", "alice")]
    [InlineData(" ", "alice", "alice")]
    [InlineData("Alice Wang", "alice", "Alice Wang")]
    public void Creator_name_prefers_display_name_then_user_name(string? name, string userName, string expected)
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        Create(id, name, userName).SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal(expected, entity.CreatorName);
        Assert.Equal(Now, entity.CreationTime);
    }

    [Fact]
    public void Identity_without_any_name_uses_id_as_label()
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        Create(id, null).SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal(id.ToString("D"), entity.CreatorName);
    }

    [Fact]
    public void Explicit_creator_does_not_receive_another_users_name()
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        entity.SetAudit(id, "", default);
        Create(Guid.NewGuid(), "Other User").SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal(id.ToString("D"), entity.CreatorName);
        Assert.Equal(Now, entity.CreationTime);
    }

    [Fact]
    public void Explicit_system_creator_works_without_current_user()
    {
        var id = Guid.NewGuid();
        var entity = new Order();
        entity.SetAudit(id, "Scheduled import", default);
        Create(null, null).SetCreationProperties(entity);
        Assert.Equal(id, entity.CreatorId);
        Assert.Equal("Scheduled import", entity.CreatorName);
        Assert.Equal(Now, entity.CreationTime);
    }
    public class TenantOrder : Order, IMultiTenant { public Guid? TenantId { get; set; } }
    public class AbpTenantOrder : AbpOrder, IMultiTenant { public Guid? TenantId { get; set; } }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Module_registration_honors_options_and_preserves_abp_tenant_audit(bool strict)
    {
        var services = new ServiceCollection();
        using var app = services.AddApplication<DedsiEntityFrameworkCoreModule>();
        if (strict) services.Configure<DedsiAuditOptions>(options => options.CheckCreatorTenant = true);
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var user = new Mock<ICurrentUser>();
        user.SetupGet(x => x.Id).Returns(id);
        user.SetupGet(x => x.TenantId).Returns(tenantId);
        user.SetupGet(x => x.Name).Returns("Alice");
        services.AddSingleton(user.Object);
        using var provider = services.BuildServiceProvider();
        var setter = provider.GetRequiredService<IAuditPropertySetter>();
        Assert.IsType<DedsiAuditPropertySetter>(setter);
        var entity = new TenantOrder { TenantId = Guid.NewGuid() };
        if (strict)
            Assert.Throws<ArgumentException>(() => setter.SetCreationProperties(entity));
        else
        {
            setter.SetCreationProperties(entity);
            Assert.Equal(id, entity.CreatorId);
            Assert.Equal("Alice", entity.CreatorName);
        }

        var abpCrossTenant = new AbpTenantOrder { TenantId = entity.TenantId };
        setter.SetCreationProperties(abpCrossTenant);
        Assert.Null(abpCrossTenant.CreatorId);
        var abpSameTenant = new AbpTenantOrder { TenantId = tenantId };
        setter.SetCreationProperties(abpSameTenant);
        Assert.Equal(id, abpSameTenant.CreatorId);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Cross_tenant_check_is_opt_in(bool strict)
    {
        var userId = Guid.NewGuid();
        var user = new Mock<ICurrentUser>();
        user.SetupGet(x => x.Id).Returns(userId);
        user.SetupGet(x => x.TenantId).Returns(Guid.NewGuid());
        user.SetupGet(x => x.Name).Returns("Alice");
        var setter = new DedsiAuditPropertySetter(user.Object, Mock.Of<ICurrentTenant>(), Mock.Of<IClock>(),
            Options.Create(new DedsiAuditOptions { CheckCreatorTenant = strict }));
        var entity = new TenantOrder { TenantId = Guid.NewGuid() };
        if (strict)
        {
            Assert.Throws<ArgumentException>(() => setter.SetCreationProperties(entity));
            Assert.Equal(Guid.Empty, entity.CreatorId);
            Assert.Null(entity.CreatorName);
        }
        else
        {
            setter.SetCreationProperties(entity);
            Assert.Equal(userId, entity.CreatorId);
            Assert.Equal("Alice", entity.CreatorName);
        }
        entity.SetAudit(userId, "", default);
        setter.SetCreationProperties(entity);
        Assert.Equal(strict ? userId.ToString("D") : "Alice", entity.CreatorName);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Strict_mode_accepts_same_tenant_and_host_identity(bool tenant)
    {
        Guid? tenantId = tenant ? Guid.NewGuid() : null;
        var userId = Guid.NewGuid();
        var user = new Mock<ICurrentUser>();
        user.SetupGet(x => x.Id).Returns(userId);
        user.SetupGet(x => x.TenantId).Returns(tenantId);
        user.SetupGet(x => x.Name).Returns("Alice");
        var setter = new DedsiAuditPropertySetter(user.Object, Mock.Of<ICurrentTenant>(), Mock.Of<IClock>(),
            Options.Create(new DedsiAuditOptions { CheckCreatorTenant = true }));
        var entity = new TenantOrder { TenantId = tenantId };
        setter.SetCreationProperties(entity);
        Assert.Equal(userId, entity.CreatorId);
        Assert.Equal("Alice", entity.CreatorName);
    }

}
