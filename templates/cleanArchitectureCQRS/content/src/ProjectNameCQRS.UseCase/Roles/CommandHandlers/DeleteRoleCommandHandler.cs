using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Roles.Commands;

namespace ProjectNameCQRS.Roles.CommandHandlers;

public class DeleteRoleCommandHandler(IRoleRepository roleRepository) : DedsiCommandHandler<DeleteRoleCommand, bool>
{
    public override async Task<bool> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        await roleRepository.DeleteAsync(a => a.Id == command.id);
        return true;
    }
}
