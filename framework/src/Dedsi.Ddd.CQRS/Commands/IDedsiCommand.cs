using MediatR;

namespace Dedsi.Ddd.CQRS;

public interface IDedsiCommand : IRequest;

public interface IDedsiCommand<out TResponse> : IRequest<TResponse>;