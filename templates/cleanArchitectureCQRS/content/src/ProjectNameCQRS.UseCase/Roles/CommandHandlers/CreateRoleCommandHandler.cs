using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Roles.Commands;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Roles.CommandHandlers;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IGuidGenerator guidGenerator) : ILocalEventHandler<CreateRoleCommand>, ITransientDependency
{
    public async Task HandleEventAsync(CreateRoleCommand eventData)
    {
        var role = new Role(guidGenerator.Create(), eventData.RoleCode, eventData.RoleName);

        await roleRepository.InsertAsync(role);
    }

}
