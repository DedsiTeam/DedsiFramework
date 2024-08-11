using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// CreateUserCommand 处理处理器
/// </summary>
/// <param name="UserRepository"></param>
/// <param name="GuidGenerator"></param>
public class CreateUserCommandHandler(IUserRepository UserRepository,IGuidGenerator GuidGenerator) 
    : DedsiCommandHandler<CreateUserCommand,Guid>
{
    public override async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(GuidGenerator.Create(), request.UserName,request.Account,"PassWork@2024",request.Email);
        await UserRepository.InsertAsync(user, cancellationToken: cancellationToken);
        return user.Id;
    }
}