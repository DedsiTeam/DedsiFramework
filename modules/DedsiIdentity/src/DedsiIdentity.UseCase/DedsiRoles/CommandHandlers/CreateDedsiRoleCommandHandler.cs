using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiRoles;

namespace DedsiIdentity.DedsiRoles.CommandHandlers;

/// <summary>
/// 创建角色命令
/// </summary>
/// <param name="RoleName">角色名称</param>
/// <param name="RoleCode">角色代码</param>
public record CreateDedsiRoleCommand(string RoleCode, string RoleName) : DedsiCommand<string>;

/// <summary>
/// 创建角色命令处理器
/// </summary>
/// <param name="dedsiRoleRepository">角色仓储</param>
public class CreateDedsiRoleCommandHandler(IDedsiRoleRepository dedsiRoleRepository) : DedsiCommandHandler<CreateDedsiRoleCommand, string>
{
    public override async Task<string> Handle(CreateDedsiRoleCommand command, CancellationToken cancellationToken)
    {
        var dedsiRole = new DedsiRole(
            Ulid.NewUlid().ToString(),
            command.RoleCode,
            command.RoleName);

        await dedsiRoleRepository.InsertAsync(dedsiRole, false, cancellationToken);

        return dedsiRole.Id;
    }
}