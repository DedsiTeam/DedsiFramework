using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Repositories.Users;

namespace ProjectNameCQRS.Users.Commands;

/// <summary>
/// CreateUserCommand 处理
/// </summary>
/// <param name="userRepository"></param>
public class CreateUserCommandHandler(IUserRepository userRepository): IDedsiCommandHandler<CreateUserCommand,Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            UserName = request.UserDto.UserName,
            Account = request.UserDto.Account,
            PassWord = request.UserDto.PassWord,
            Email = request.UserDto.Email,
        };
        await userRepository.CreateAsync(user);
        return user.Id;
    }
}