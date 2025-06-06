using Dedsi.CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using DedsiIdentity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DedsiIdentity;

[DependsOn(
    typeof(DedsiCleanArchitectureInfrastructureModule)
)]
public class DedsiIdentityInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // EntityFrameworkCore
        context.Services.AddAbpDbContext<DedsiIdentityDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}