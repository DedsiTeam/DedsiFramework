using Dedsi.Ddd.CQRS.Commands;
using MediatR;

namespace Dedsi.Ddd.CQRS.Mediators;

public class DedsiMediator(IMediator mediator) : IDedsiMediator
{
    public virtual Task PublishAsync(IDedsiCommand command,CancellationToken cancellationToken = default)
    {
        return mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task<TResponse> PublishAsync<TResponse>(IDedsiCommand<TResponse> command,CancellationToken cancellationToken = default)
    {
        return mediator.Send(command, cancellationToken);
    }

}