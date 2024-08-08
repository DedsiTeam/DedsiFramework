using ProjectNameCQRS.Repositories.Users;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace ProjectNameCQRS.Users.Commands;

/// <summary>
/// CreateUserCommand 处理
/// </summary>
/// <param name="userRepository"></param>
public class CreateUserCommandHandler(IUserRepository userRepository): ILocalEventHandler<CreateUserCommand>, ITransientDependency
{
    public Task HandleEventAsync(CreateUserCommand eventData)
    {
        return userRepository.CreateAsync(new User()
        {
            UserName = eventData.UserDto.UserName,
            Account = eventData.UserDto.Account,
            PassWord = eventData.UserDto.PassWord,
            Email = eventData.UserDto.Email,
        });
    }
}