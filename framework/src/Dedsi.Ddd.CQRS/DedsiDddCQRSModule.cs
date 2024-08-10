using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dedsi.Ddd.CQRS;

public class DedsiDddCQRSModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IDedsiMediator,DedsiMediator>();
    }
}