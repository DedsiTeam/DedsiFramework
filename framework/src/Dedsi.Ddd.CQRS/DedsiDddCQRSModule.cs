using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.CQRS;

[DependsOn(
    typeof(AbpEventBusAbstractionsModule)    
)]
public class DedsiDddCQRSModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IDedsiMediator,DedsiMediator>();
    }
}