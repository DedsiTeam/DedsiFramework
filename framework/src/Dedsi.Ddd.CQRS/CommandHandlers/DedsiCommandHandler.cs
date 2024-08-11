using Dedsi.Ddd.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.CQRS.CommandHandlers;

public class DedsiCommandHandler: IDedsiCommandHandler
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
   
    protected ILoggerFactory LoggerFactory
    {
        get => this.LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
    }

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName!) ?? NullLogger.Instance);
}

public abstract class DedsiCommandHandler<TCommand> : DedsiCommandHandler, IDedsiCommandHandler<TCommand>
    where TCommand : DedsiCommand
{
    public abstract Task Handle(TCommand request, CancellationToken cancellationToken);
}

public abstract class DedsiCommandHandler<TCommand, TResponse> : DedsiCommandHandler, IDedsiCommandHandler<TCommand, TResponse>
    where TCommand : DedsiCommand<TResponse>
{
    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
}