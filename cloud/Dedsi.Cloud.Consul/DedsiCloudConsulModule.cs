using Dedsi.AspNetCore;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Dedsi.Cloud.Consul;

[DependsOn(
    typeof(DedsiAspNetCoreModule)    
)]
public class DedsiCloudConsulModule : AbpModule
{
    public virtual void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseConsul();
    }
}