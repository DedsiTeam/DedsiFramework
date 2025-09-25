using Dedsi.Ddd.CQRS.CommandEventRecorders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Dedsi.Ddd.CQRS.EventBus;

[ExposeServices(typeof(ILocalEventBus), typeof(DedsiLocalEventBus))]
public class DedsiLocalEventBus(
        ICqrsCeRecorder cqrsCeRecorder,
        IOptions<AbpLocalEventBusOptions> options,
        IServiceScopeFactory serviceScopeFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IEventHandlerInvoker eventHandlerInvoker) 
    : LocalEventBus(options, serviceScopeFactory, currentTenant, unitOfWorkManager, eventHandlerInvoker)
{

    private string[] _abpEventDataNames = [
        typeof(EntityCreatedEventData<object>).Name,
        typeof(EntityDeletedEventData<object>).Name,
        typeof(EntityUpdatedEventData<object>).Name,
    ];

    /// <summary>
    /// 需要发布之前记录日志
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventData"></param>
    /// <param name="onUnitOfWorkComplete"></param>
    /// <returns></returns>
    public override async Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true)
    {
        var cancellationToken = CancellationToken.None;

        if (eventData is IDedsiEvent dedsiEvent)
        {
            await cqrsCeRecorder.RecorderAsync(
                dedsiEvent.EventId.Value, 
                eventType.Name, 
                eventType.FullName,
                RecorderDataSource.Event,
                cancellationToken);
        }
        else if (_abpEventDataNames.Any(a => a == eventType.Name))
        {
            var entity = eventType.GetProperty("Entity").GetValue(eventData);

            var a = entity.GetType().BaseType.Name;

            // 如果是聚合根
            if (entity.GetType().BaseType.Name == typeof(AggregateRoot<object>).Name)
            {
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(entity);

                    if (idValue is Guid guid) {
                        await cqrsCeRecorder.RecorderAsync(
                            guid,
                            eventType.Name,
                            eventType.FullName,
                            RecorderDataSource.Event,
                            cancellationToken);
                    }

                }
            }
        }

        await base.PublishAsync(eventType, eventData, onUnitOfWorkComplete);
    }
}
