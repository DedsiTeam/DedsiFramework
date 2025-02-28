using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using ProjectNameCQRS.Repositories.Users;

namespace ProjectNameCQRS.Users.CommandHandlers;

/// <summary>
/// 命令：创建用户
/// </summary>
/// <param name="UserName"></param>
/// <param name="Account"></param>
/// <param name="Email"></param>
public record CreateUserCommand(string UserName, string Account, string Email) : DedsiCommand<Guid>;

public class CreateUserCommandHandler(IUserRepository userRepository)
    : DedsiCommandHandler<CreateUserCommand, Guid>
{
    public override async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // 创建用户
        var user = new User(GuidGenerator.Create(), command.UserName, command.Account, "PassWord@" + DateTime.Now.Year, command.Email);

        // 保存到数据库
        await userRepository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id;
    }
}