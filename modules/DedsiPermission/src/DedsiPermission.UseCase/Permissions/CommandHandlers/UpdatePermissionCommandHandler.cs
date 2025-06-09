using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiPermission.Repositories.Permissions;

namespace DedsiPermission.Permissions.CommandHandlers;

/// <summary>
/// 更新权限命令
/// </summary>
/// <param name="Id">权限ID</param>
/// <param name="PermissionName">权限名称</param>
/// <param name="PermissionDescription">权限描述</param>
/// <param name="PermissionType">权限类型</param>
/// <param name="PermissionGroupCode">权限组代码</param>
/// <param name="PermissionGroupName">权限组名称</param>
/// <param name="ParentId">父权限ID</param>
public record UpdatePermissionCommand(
    string Id,
    string PermissionName,
    string PermissionDescription,
    PermissionType PermissionType,
    string PermissionGroupCode,
    string PermissionGroupName,
    string? ParentId = null) : DedsiCommand<bool>;

/// <summary>
/// 更新权限命令处理器
/// </summary>
/// <param name="permissionRepository">权限仓储</param>
public class UpdatePermissionCommandHandler(IPermissionRepository permissionRepository) : DedsiCommandHandler<UpdatePermissionCommand, bool>
{
    public override async Task<bool> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetAsync(a => a.Id == command.Id, true, cancellationToken);
        if (permission == null)
        {
            return false;
        }

        permission.ChangePermissionName(command.PermissionName);
        permission.ChangePermissionDescription(command.PermissionDescription);
        permission.ChangePermissionType(command.PermissionType);
        permission.ChangePermissionGroupCode(command.PermissionGroupCode);
        permission.ChangePermissionGroupName(command.PermissionGroupName);
        permission.ChangeParentId(command.ParentId);

        await permissionRepository.UpdateAsync(permission, false, cancellationToken);

        return true;
    }
}