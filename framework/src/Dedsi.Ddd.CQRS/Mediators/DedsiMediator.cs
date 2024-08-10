using MediatR;

namespace Dedsi.Ddd.CQRS.Mediators;

public class DedsiMediator : Mediator, IDedsiMediator
{
    public DedsiMediator(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public DedsiMediator(IServiceProvider serviceProvider, INotificationPublisher publisher) : base(serviceProvider, publisher)
    {
    }
}