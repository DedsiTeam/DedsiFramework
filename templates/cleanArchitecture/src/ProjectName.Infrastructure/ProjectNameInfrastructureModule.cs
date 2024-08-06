using Microsoft.Extensions.DependencyInjection;
using ProjectName.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ProjectName.Infrastructure;

[DependsOn(
    typeof(AbpEntityFrameworkCoreModule)
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