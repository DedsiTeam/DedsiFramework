using Dedsi.CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using DedsiPermission.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DedsiPermission;

[DependsOn(
    typeof(DedsiCleanArchitectureInfrastructureModule)
)]
public class DedsiPermissionInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // EntityFrameworkCore
        context.Services.AddAbpDbContext<DedsiPermissionDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}