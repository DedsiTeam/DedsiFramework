using MediatR;

namespace Dedsi.Ddd.CQRS;

public interface IDedsiCommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : IDedsiCommand;

public interface IDedsiCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IDedsiCommand<TResponse>;