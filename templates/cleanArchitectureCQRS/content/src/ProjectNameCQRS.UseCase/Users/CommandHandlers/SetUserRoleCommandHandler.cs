using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// SetUserRoleCommand
/// </summary>
/// <param name="userRepository"></param>
/// <param name="roleRepository"></param>
/// <param name="guidGenerator"></param>
public class SetUserRoleCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IGuidGenerator guidGenerator)
    : ILocalEventHandler<SetUserRoleCommand>, ITransientDependency
{
    public async Task HandleEventAsync(SetUserRoleCommand eventData)
    {
        var user = await userRepository.GetAsync(a => a.Id == eventData.userId);

        foreach (var role in eventData.Roles)
        {
            var isAny = await roleRepository.AnyAsync(a => a.Id == role.Id);
            if (!isAny)
            {
                throw new UserFriendlyException("角色不存在！role.Id = " + role.Id);
            }
            user.SetUserRole(new UserRole(guidGenerator.Create(), user.Id, role.Id, role.RoleName));
        }
    }
}
