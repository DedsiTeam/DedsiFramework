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
            // context.Services.AddAbpDbContext<SystemManagementDbContext>(options =>
            // {
            //     options.AddDefaultRepositories(true);
            // });
        }

}