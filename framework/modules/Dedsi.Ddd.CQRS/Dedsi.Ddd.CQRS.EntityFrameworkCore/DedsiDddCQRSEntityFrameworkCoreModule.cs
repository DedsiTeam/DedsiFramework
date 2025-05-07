using Dedsi.Ddd.CQRS.EntityFrameworkCore.EntityFrameworkCores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.CQRS.EntityFrameworkCore;

[DependsOn(
    typeof(DedsiDddCqrsModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DedsiDddCQRSEntityFrameworkCoreModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        // EntityFrameworkCore
        context.Services.AddAbpDbContext<DedsiDddCqrsDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}