using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiRoles;

namespace DedsiIdentity.DedsiRoles.CommandHandlers;

/// <summary>
/// 更新角色命令
/// </summary>
/// <param name="Id">角色ID</param>
/// <param name="RoleCode">角色代码</param>
/// <param name="RoleName">角色名称</param>
public record UpdateDedsiRoleCommand(string Id, string RoleCode, string RoleName) : DedsiCommand<string>;

/// <summary>
/// 更新角色命令处理器
/// </summary>
/// <param name="dedsiRoleRepository">角色仓储</param>
public class UpdateDedsiRoleCommandHandler(IDedsiRoleRepository dedsiRoleRepository) : DedsiCommandHandler<UpdateDedsiRoleCommand, string>
{
    public override async Task<string> Handle(UpdateDedsiRoleCommand command, CancellationToken cancellationToken)
    {
        var dedsiRole = await dedsiRoleRepository.GetAsync(a => a.Id == command.Id, true, cancellationToken);

        dedsiRole.ChangeRoleCode(command.RoleCode);
        dedsiRole.ChangeRoleName(command.RoleName);

        await dedsiRoleRepository.UpdateAsync(dedsiRole, false, cancellationToken);

        return command.Id;
    }
}