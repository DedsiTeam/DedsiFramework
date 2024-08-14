using Dedsi.Ddd.CQRS.Commands;
using MediatR;
using Volo.Abp.EventBus.Local;

namespace Dedsi.Ddd.CQRS.Mediators;

public class DedsiMediator(ILocalEventBus localEventBus, IMediator mediator) : IDedsiMediator
{

    /// <inheritdoc />
    public Task PublishAsync(IDedsiCommand command, bool onUnitOfWorkComplete = true)
    {
        return localEventBus.PublishAsync(command, onUnitOfWorkComplete);
    }

    /// <inheritdoc />
    public virtual Task<TResponse> PublishAsync<TResponse>(IDedsiCommand<TResponse> command,CancellationToken cancellationToken = default)
    {
        return mediator.Send(command, cancellationToken);
    }

}