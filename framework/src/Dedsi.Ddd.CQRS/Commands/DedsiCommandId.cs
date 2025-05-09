namespace Dedsi.Ddd.CQRS.Commands;

public record DedsiCommandId(Guid Value);

public static class DedsiCommandIdExtensions
{
    public static DedsiCommandId ToDedsiEventId(this Guid guid)
    {
        return new DedsiCommandId(guid);
    }
}