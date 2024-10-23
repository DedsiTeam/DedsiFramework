using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Roles.Commands;
using Volo.Abp;

namespace ProjectNameCQRS.Roles.CommandHandlers;

public class UpdateRoleCommandHandler(IRoleRepository roleRepository) : DedsiCommandHandler<UpdateRoleCommand, bool>
{
    public override async Task<bool> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetAsync(a => a.Id == command.id, cancellationToken: cancellationToken);
        
        role.Update(command.RoleCode, command.RoleName);

        await roleRepository.UpdateAsync(role, cancellationToken: cancellationToken);

        return true;
    }
}
