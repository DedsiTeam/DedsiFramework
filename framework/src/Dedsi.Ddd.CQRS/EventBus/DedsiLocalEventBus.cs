using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Dedsi.Ddd.CQRS.EventBus;

[ExposeServices(typeof(ILocalEventBus), typeof(DedsiLocalEventBus))]
public class DedsiLocalEventBus(
        ILogger<DedsiLocalEventBus> logger,
        IOptions<AbpLocalEventBusOptions> options,
        IServiceScopeFactory serviceScopeFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IEventHandlerInvoker eventHandlerInvoker) 
    : LocalEventBus(options, serviceScopeFactory, currentTenant, unitOfWorkManager, eventHandlerInvoker)
{
    /// <summary>
    /// 需要发布之前记录日志
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventData"></param>
    /// <param name="onUnitOfWorkComplete"></param>
    /// <returns></returns>
    public override Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true)
    {
        if (eventData is IDedsiEvent dedsiEvent)
        {
            logger.LogInformation("---------------------------------------- ILocalEventBus PublishAsync() -------------------------------------------------------");
            logger.LogInformation($"EventId = {dedsiEvent.EventId.EventId}");
            logger.LogInformation($"EventName = {eventType.Name}");
            logger.LogInformation(eventType.FullName);
            logger.LogInformation("---------------------------------------- ILocalEventBus PublishAsync() -------------------------------------------------------");
        }

        return base.PublishAsync(eventType, eventData, onUnitOfWorkComplete);
    }
}
