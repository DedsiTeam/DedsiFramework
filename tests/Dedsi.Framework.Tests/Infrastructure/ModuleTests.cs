using Dedsi.Core;
using Dedsi.Ddd.Domain;
using Dedsi.Ddd.Domain.Shared;
using Dedsi.Ddd.Application;
using Dedsi.Ddd.Application.Contracts;
using Dedsi.EntityFrameworkCore;
using Dedsi.EntityFrameworkCore.PropertySetters;
using Dedsi.CleanArchitecture.Domain;
using Dedsi.CleanArchitecture.HttpApi;
using Dedsi.CleanArchitecture.Infrastructure;
using Dedsi.AspNetCore;
using Dedsi.SqlSugar;
using Dedsi.FastEndpoints;
using Dedsi.CAP;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Application;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Modularity;

namespace Dedsi.Framework.Tests.Infrastructure;

[Trait("Category", "Integration")]
public class ModuleTests
{
    public static IEnumerable<object[]> StandaloneModules()
    {
        yield return [typeof(DedsiCoreModule), new Type[] { typeof(Volo.Abp.Security.AbpSecurityModule) }];
        yield return [typeof(DedsiDddDomainSharedModule), new Type[] { typeof(Volo.Abp.Domain.AbpDddDomainSharedModule) }];
        yield return [typeof(DedsiDddDomainModule), new Type[] { typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiDddApplicationContractsModule), new Type[] { typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule), typeof(Volo.Abp.FluentValidation.AbpFluentValidationModule) }];
        yield return [typeof(DedsiDddApplicationModule), new Type[] { typeof(DedsiDddApplicationContractsModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiEntityFrameworkCoreModule), new Type[] { typeof(DedsiDddDomainModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiAspNetCoreModule), new Type[] { typeof(DedsiDddApplicationContractsModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiSqlSugarModule), new Type[] { typeof(DedsiDddDomainModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiCapModule), new Type[] { typeof(DedsiAspNetCoreModule), typeof(DedsiDddApplicationContractsModule) }];
        yield return [typeof(DedsiFastEndpointsModule), new Type[] { typeof(DedsiAspNetCoreModule), typeof(DedsiDddApplicationContractsModule) }];
        yield return [typeof(DedsiCleanArchitectureDomainModule), new Type[] { typeof(DedsiDddDomainModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiCleanArchitectureInfrastructureModule), new Type[] { typeof(DedsiCleanArchitectureDomainModule), typeof(DedsiEntityFrameworkCoreModule), typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule) }];
        yield return [typeof(DedsiCleanArchitectureHttpApiModule), new Type[] { typeof(DedsiAspNetCoreModule), typeof(DedsiDddApplicationContractsModule) }];
    }

    [Theory]
    [MemberData(nameof(StandaloneModules))]
    public void Standalone_module_loads_its_dependencies_in_order(Type entryModule, Type[] expectedDependencies)
    {
        var services = new ServiceCollection();
        using var app = services.AddApplication(entryModule);
        var modules = app.Modules.Select(module => module.Type).ToList();
        Assert.Equal(modules.Count, modules.Distinct().Count());
        var entryIndex = modules.IndexOf(entryModule);
        Assert.True(entryIndex >= 0);
        foreach (var dependency in expectedDependencies)
        {
            Assert.Contains(dependency, modules);
            Assert.True(modules.IndexOf(dependency) < entryIndex, $"{dependency.Name} must load before {entryModule.Name}");
        }
    }

    [Fact]
    public void Combined_modules_register_framework_services()
    {
        var services = new ServiceCollection();
        using var app = services.AddApplication<AllModules>();
        using var provider = services.BuildServiceProvider();
        Assert.IsType<DedsiAuditPropertySetter>(provider.GetRequiredService<IAuditPropertySetter>());
        Assert.IsType<LocalEventBus>(provider.GetRequiredService<ILocalEventBus>());
        Assert.Contains(app.Modules, module => module.Type == typeof(DedsiDddApplicationModule));
        Assert.Contains(app.Modules, module => module.Type == typeof(AbpDddApplicationModule));
    }

    [Fact]
    public void Domain_module_loads_without_application_layer()
    {
        var services = new ServiceCollection();
        using var app = services.AddApplication<DedsiCleanArchitectureDomainModule>();
        Assert.Contains(app.Modules, module => module.Type == typeof(DedsiDddDomainModule));
        AssertNoApplicationModules(app.Modules);
    }

    [Fact]
    public void Infrastructure_module_does_not_pull_in_application_layer()
    {
        var services = new ServiceCollection();
        using var app = services.AddApplication<DedsiCleanArchitectureInfrastructureModule>();
        Assert.Contains(app.Modules, module => module.Type == typeof(DedsiCleanArchitectureDomainModule));
        Assert.Contains(app.Modules, module => module.Type == typeof(DedsiEntityFrameworkCoreModule));
        AssertNoApplicationModules(app.Modules);
    }

    private static void AssertNoApplicationModules(IReadOnlyList<IAbpModuleDescriptor> modules)
    {
        Type[] applicationModules = [typeof(DedsiDddApplicationModule), typeof(DedsiDddApplicationContractsModule),
            typeof(AbpDddApplicationModule), typeof(AbpDddApplicationContractsModule)];
        Assert.DoesNotContain(modules, module => applicationModules.Contains(module.Type));
    }
}

[DependsOn(typeof(DedsiCoreModule), typeof(DedsiDddDomainSharedModule), typeof(DedsiDddDomainModule),
    typeof(DedsiDddApplicationContractsModule), typeof(DedsiDddApplicationModule),
    typeof(DedsiEntityFrameworkCoreModule), typeof(DedsiAspNetCoreModule), typeof(DedsiSqlSugarModule),
    typeof(DedsiCleanArchitectureDomainModule), typeof(DedsiCleanArchitectureInfrastructureModule),
    typeof(DedsiCleanArchitectureHttpApiModule), typeof(DedsiCapModule), typeof(DedsiFastEndpointsModule))]
public class AllModules : AbpModule
{
    public AllModules() => SkipAutoServiceRegistration = true;
}
