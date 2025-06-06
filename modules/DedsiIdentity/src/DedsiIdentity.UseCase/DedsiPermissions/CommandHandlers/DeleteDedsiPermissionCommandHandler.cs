using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiPermissions;

namespace DedsiIdentity.DedsiPermissions.CommandHandlers;

public record DeleteDedsiPermissionCommand(string DedsiPermissionId) : DedsiCommand<bool>;

public class DeleteDedsiPermissionCommandHandler(IDedsiPermissionRepository dedsiPermissionRepository) : DedsiCommandHandler<DeleteDedsiPermissionCommand, bool>
{
    public override async Task<bool> Handle(DeleteDedsiPermissionCommand command, CancellationToken cancellationToken)
    {
        var dedsiPermission = await dedsiPermissionRepository.GetAsync(a => a.Id == command.DedsiPermissionId, true, cancellationToken);

        await dedsiPermissionRepository.DeleteAsync(dedsiPermission, false, cancellationToken);

        return true;
    }
}