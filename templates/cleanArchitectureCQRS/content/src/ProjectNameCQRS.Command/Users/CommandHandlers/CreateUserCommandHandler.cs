using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// CreateUserCommand 处理
/// </summary>
/// <param name="userRepository"></param>
public class CreateUserCommandHandler(IUserRepository userRepository) : IDedsiCommandHandler<CreateUserCommand,Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), request.UserDto.UserName, request.UserDto.Account, request.UserDto.PassWord, request.UserDto.Email);
        await userRepository.CreateAsync(user);
        return user.Id;
    }
}