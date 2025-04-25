namespace Dedsi.Ddd.CQRS.EventBus;

/// <summary>
/// 事件Id
/// </summary>
/// <param name="Id"></param>
public record DedsiEventId(Guid EventId);

public static class DedsiEventIdExtensions
{
    public static DedsiEventId ToDedsiEventId(this Guid guid)
    {
        return new DedsiEventId(guid);
    }
}