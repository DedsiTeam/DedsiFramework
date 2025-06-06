using Dedsi.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DedsiIdentity;

[DependsOn(
    typeof(DedsiIdentityUseCaseModule),
    typeof(DedsiAspNetCoreModule)
)]
public class DedsiIdentityHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DedsiIdentityHttpApiModule).Assembly);
        });
    }
}