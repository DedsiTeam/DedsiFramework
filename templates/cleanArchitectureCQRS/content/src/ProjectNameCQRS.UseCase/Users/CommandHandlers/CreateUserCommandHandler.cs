using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// CreateUserCommand 处理处理器
/// </summary>
/// <param name="userRepository"></param>
/// <param name="roleRepository"></param>
/// <param name="guidGenerator"></param>
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IGuidGenerator guidGenerator)
    : ILocalEventHandler<CreateUserCommand>, ITransientDependency
{
    public async Task HandleEventAsync(CreateUserCommand eventData)
    {
        // 普通用户角色
        var role = await roleRepository.GetAsync(a => a.RoleCode == "OrdinaryUser");

        // 创建用户
        var user = new User(guidGenerator.Create(), eventData.UserName, eventData.Account, "PassWork@" + DateTime.Now.Year, eventData.Email, role);

        // 保存到数据库
        await userRepository.InsertAsync(user);
    }
}