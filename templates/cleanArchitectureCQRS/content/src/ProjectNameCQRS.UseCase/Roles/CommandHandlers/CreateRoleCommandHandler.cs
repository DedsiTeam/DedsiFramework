using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Roles.Commands;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Roles.CommandHandlers;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IGuidGenerator guidGenerator) : DedsiCommandHandler<CreateRoleCommand, Guid>
{
    public override async Task<Guid> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = new Role(guidGenerator.Create(), command.RoleCode, command.RoleName);

        await roleRepository.InsertAsync(role, cancellationToken: cancellationToken);

        return role.Id;
    }
}
