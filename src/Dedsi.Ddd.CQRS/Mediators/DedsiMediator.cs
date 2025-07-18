using Dedsi.Ddd.CQRS.CommandEventRecorders;
using Dedsi.Ddd.CQRS.Commands;
using MediatR;

namespace Dedsi.Ddd.CQRS.Mediators;

public class DedsiMediator(
    ICqrsCeRecorder cqrsCeRecorder,
    IMediator mediator) : IDedsiMediator
{
    /// <inheritdoc />
    public virtual async Task SendAsync(IDedsiCommand command, CancellationToken cancellationToken = default)
    {
        await cqrsCeRecorder.RecorderAsync(
            command.CommandId.Value, 
            command.GetType().Name, 
            command.GetType().FullName,
            RecorderDataSource.Command,
            cancellationToken);

        await mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<TResponse> SendAsync<TResponse>(IDedsiCommand<TResponse> command,CancellationToken cancellationToken = default)
    {
        await cqrsCeRecorder.RecorderAsync(
            command.CommandId.Value,
            command.GetType().Name,
            command.GetType().FullName,
            RecorderDataSource.Command,
            cancellationToken);

        return await mediator.Send(command, cancellationToken);
    }

}