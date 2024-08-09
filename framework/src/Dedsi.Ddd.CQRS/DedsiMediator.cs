using MediatR;

namespace Dedsi.Ddd.CQRS;

public interface IDedsiMediator : IMediator;


public class DedsiMediator : Mediator, IDedsiMediator
{
    public DedsiMediator(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public DedsiMediator(IServiceProvider serviceProvider, INotificationPublisher publisher) : base(serviceProvider, publisher)
    {
    }
}