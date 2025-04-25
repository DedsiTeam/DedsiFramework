using Dedsi.Ddd.CQRS.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dedsi.Ddd.CQRS.Mediators;

public class DedsiMediator(ILogger<DedsiMediator> logger, IMediator mediator) : IDedsiMediator
{
    /// <inheritdoc />
    public virtual Task SendAsync(IDedsiCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("---------------------------------------- IDedsiMediator SendAsync() -------------------------------------------------------");
        logger.LogInformation($"CommandId = {command.CommandId.CommandId}");
        logger.LogInformation($"CommandName = {command.GetType().Name}");
        logger.LogInformation(command.GetType().FullName);
        logger.LogInformation("---------------------------------------- IDedsiMediator SendAsync() -------------------------------------------------------");

        return mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task<TResponse> SendAsync<TResponse>(IDedsiCommand<TResponse> command,CancellationToken cancellationToken = default)
    {
        logger.LogInformation("---------------------------------------- IDedsiMediator SendAsync<TResponse>() -------------------------------------------------------");
        logger.LogInformation($"CommandId = {command.CommandId.CommandId}");
        logger.LogInformation($"CommandName = {command.GetType().Name}");
        logger.LogInformation(command.GetType().FullName);
        logger.LogInformation("---------------------------------------- IDedsiMediator SendAsync<TResponse>() -------------------------------------------------------");

        return mediator.Send(command, cancellationToken);
    }

}