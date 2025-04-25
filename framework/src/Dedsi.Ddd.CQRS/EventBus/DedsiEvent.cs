namespace Dedsi.Ddd.CQRS.EventBus;

public class DedsiEvent: IDedsiEvent
{

    public DedsiEvent() 
    {
        SetEventId(new DedsiEventId(Guid.CreateVersion7()));
    }

    public DedsiEvent(DedsiEventId eventId)
    {
        SetEventId(eventId);
    }

    public DedsiEventId EventId { get; private set; }

    public void SetEventId(DedsiEventId eventId)
    {
        EventId = eventId;
    }
}
