using Dedsi.CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ProjectName;

[DependsOn(
    typeof(DedsiCleanArchitectureInfrastructureModule)
)]
public class ProjectNameInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ProjectNameDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}