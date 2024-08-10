using MediatR;

namespace Dedsi.Ddd.CQRS.Commands;

public interface IDedsiCommand : IRequest;

public interface IDedsiCommand<out TResponse> : IRequest<TResponse>;