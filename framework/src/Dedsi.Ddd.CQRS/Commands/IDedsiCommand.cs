using MediatR;

namespace Dedsi.Ddd.CQRS.Commands;

public interface IDedsiCommand : IRequest
{
    DedsiCommandId CommandId { get; }
}

public interface IDedsiCommand<out TResponse> : IRequest<TResponse>
{
    DedsiCommandId CommandId { get; }
}