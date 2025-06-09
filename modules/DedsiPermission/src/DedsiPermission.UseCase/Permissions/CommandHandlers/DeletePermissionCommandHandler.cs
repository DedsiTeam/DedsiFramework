using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiPermission.Repositories.Permissions;

namespace DedsiPermission.Permissions.CommandHandlers;

/// <summary>
/// 删除权限命令
/// </summary>
/// <param name="PermissionId">权限ID</param>
public record DeletePermissionCommand(string PermissionId) : DedsiCommand<bool>;

/// <summary>
/// 删除权限命令处理器
/// </summary>
/// <param name="permissionRepository">权限仓储</param>
public class DeletePermissionCommandHandler(IPermissionRepository permissionRepository) : DedsiCommandHandler<DeletePermissionCommand, bool>
{
    public override async Task<bool> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetAsync(a => a.Id == command.PermissionId, true, cancellationToken);

        await permissionRepository.DeleteAsync(permission, false, cancellationToken);

        return true;
    }
}