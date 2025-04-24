using System;

namespace Dedsi.Ddd.CQRS.Commands;

public record DedsiCommand : IDedsiCommand
{
    public DedsiCommandId CommandId { get; private set; } = new DedsiCommandId(Guid.CreateVersion7());

    public void SetCommandId(Guid guid)
    {
        CommandId = new DedsiCommandId(guid);
    }

    public void SetCommandId(DedsiCommandId dedsiCommandId)
    {
        CommandId = dedsiCommandId;
    }
}

public record DedsiCommand<TResponse> : IDedsiCommand<TResponse>
{
    public DedsiCommandId CommandId { get; private set; } = new DedsiCommandId(Guid.CreateVersion7());

    public void SetCommandId(Guid guid)
    {
        CommandId = new DedsiCommandId(guid);
    }

    public void SetCommandId(DedsiCommandId dedsiCommandId)
    {
        CommandId = dedsiCommandId;
    }
}