using Dedsi.Ddd.CQRS.Commands;
using MediatR;

namespace Dedsi.Ddd.CQRS.CommandHandlers;

public interface IDedsiCommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : IDedsiCommand;

public interface IDedsiCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IDedsiCommand<TResponse>;