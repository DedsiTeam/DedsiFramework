using Dedsi.CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using DedsiApiGateway.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DedsiApiGateway;

[DependsOn(
    typeof(DedsiCleanArchitectureInfrastructureModule)
)]
public class DedsiApiGatewayInfrastructureModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // EntityFrameworkCore
        context.Services.AddAbpDbContext<DedsiApiGatewayDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}