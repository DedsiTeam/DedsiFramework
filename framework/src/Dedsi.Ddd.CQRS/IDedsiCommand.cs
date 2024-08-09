using MediatR;

namespace Dedsi.Ddd.CQRS;

public interface IDedsiBaseCommand;

public interface IDedsiCommand : IRequest, IDedsiBaseCommand;


public interface IDedsiCommand<out TResponse> : IRequest<TResponse>, IDedsiBaseCommand;