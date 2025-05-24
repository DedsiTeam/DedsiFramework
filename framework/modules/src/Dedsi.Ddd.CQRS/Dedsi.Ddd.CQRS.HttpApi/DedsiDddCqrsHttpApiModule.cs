using Dedsi.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.CQRS.HttpApi;

[DependsOn(
    typeof(DedsiAspNetCoreModule)
)]
public class DedsiDddCqrsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DedsiDddCqrsHttpApiModule).Assembly);
        });
    }
}
