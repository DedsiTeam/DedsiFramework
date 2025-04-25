namespace Dedsi.Ddd.CQRS.EventBus;

public interface IDedsiEvent
{
    DedsiEventId EventId { get; }
}