using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;
using Volo.Abp.Security;
using Volo.Abp.Timing;

namespace Dedsi.Ddd.CQRS;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(AbpTimingModule),
    typeof(AbpSecurityModule),
    typeof(AbpEventBusAbstractionsModule)    
)]
public class DedsiDddCqrsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IDedsiMediator,DedsiMediator>();
    }
}