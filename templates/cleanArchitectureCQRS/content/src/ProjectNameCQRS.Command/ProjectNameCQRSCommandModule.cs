using Dedsi.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ProjectNameCQRS;

[DependsOn(
    // ProjectNameCQRS
    typeof(ProjectNameCQRSDomainModule),
    typeof(ProjectNameCQRSInfrastructureModule),
    
    typeof(DedsiAspNetCoreModule)
)]
public class ProjectNameCQRSCommandModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(ProjectNameCQRSCommandModule).Assembly);
        });
    }
}