using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.CQRS;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule)    
)]
public class DedsiDddCQRSModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
    }
}