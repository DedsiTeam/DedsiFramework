using Dedsi.Ddd.CQRS.Commands;
using MediatR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Dedsi.Ddd.CQRS.CommandHandlers;


public interface IDedsiCommandHandler : ITransientDependency;

public interface IDedsiCommandHandler<in TCommand> : IDedsiCommandHandler, ILocalEventHandler<TCommand> where TCommand : IDedsiCommand;

public interface IDedsiCommandHandler<in TCommand, TResponse> : IDedsiCommandHandler, IRequestHandler<TCommand, TResponse> where TCommand : IDedsiCommand<TResponse>;