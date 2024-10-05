using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Roles.Commands;
using Volo.Abp;

namespace ProjectNameCQRS.Roles.CommandHandlers;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository) : DedsiCommandHandler<UpdateRoleCommand, bool>
{
    public override async Task<bool> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetAsync(a => a.Id == command.id);
        if (role == null)
        {
            throw new UserFriendlyException("数据不存在！");
        }
        role.Update(command.RoleCode, command.RoleName);

        await roleRepository.UpdateAsync(role);

        return true;
    }
}
