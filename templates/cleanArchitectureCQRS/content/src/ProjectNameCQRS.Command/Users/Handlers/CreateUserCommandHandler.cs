using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;

namespace ProjectNameCQRS.Users.Handlers;

/// <summary>
/// CreateUserCommand 处理
/// </summary>
/// <param name="userRepository"></param>
public class CreateUserCommandHandler(IUserRepository userRepository): IDedsiCommandHandler<CreateUserCommand,Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid())
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