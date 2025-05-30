using Dedsi.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DedsiApiGateway;

[DependsOn(
    typeof(DedsiApiGatewayUseCaseModule),
    typeof(DedsiAspNetCoreModule)
)]
public class DedsiApiGatewayHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DedsiApiGatewayHttpApiModule).Assembly);
        });
    }
}