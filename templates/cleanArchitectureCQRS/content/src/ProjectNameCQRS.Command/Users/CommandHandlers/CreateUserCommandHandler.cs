using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// CreateUserCommand 处理
/// </summary>
/// <param name="userRepository"></param>
/// <param name="guidGenerator"></param>
public class CreateUserCommandHandler(IUserRepository userRepository,IGuidGenerator guidGenerator) : IDedsiCommandHandler<CreateUserCommand,Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(guidGenerator.Create(), request.UserName,request.Account,"PassWork@2024",request.Email);
        await userRepository.CreateAsync(user);
        return user.Id;
    }
}