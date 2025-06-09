using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiPermission.Repositories.Permissions;

namespace DedsiPermission.Permissions.CommandHandlers;

/// <summary>
/// 创建权限命令
/// </summary>
/// <param name="PermissionName">权限名称</param>
/// <param name="PermissionDescription">权限描述</param>
/// <param name="PermissionType">权限类型</param>
/// <param name="PermissionGroupCode">权限组代码</param>
/// <param name="PermissionGroupName">权限组名称</param>
/// <param name="ParentId">父权限ID</param>
public record CreatePermissionCommand(
    string PermissionName,
    string PermissionDescription,
    PermissionType PermissionType,
    string PermissionGroupCode,
    string PermissionGroupName,
    string? ParentId = null) : DedsiCommand<string>;

/// <summary>
/// 创建权限命令处理器
/// </summary>
/// <param name="permissionRepository">权限仓储</param>
public class CreatePermissionCommandHandler(IPermissionRepository permissionRepository) : DedsiCommandHandler<CreatePermissionCommand, string>
{
    public override async Task<string> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        var permission = new Permission(
            Ulid.NewUlid().ToString(),
            command.PermissionName,
            command.PermissionDescription,
            command.PermissionType,
            command.PermissionGroupCode,
            command.PermissionGroupName,
            command.ParentId
        );

        await permissionRepository.InsertAsync(permission, false, cancellationToken);

        return permission.Id;
    }
}