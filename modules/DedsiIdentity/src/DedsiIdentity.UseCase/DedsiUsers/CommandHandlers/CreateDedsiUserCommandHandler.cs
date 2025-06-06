using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiUsers;

namespace DedsiIdentity.DedsiUsers.CommandHandlers;

/// <summary>
/// 创建用户命令
/// </summary>
/// <param name="UserName">用户名</param>
/// <param name="LoginPwd">密码</param>
/// <param name="LoginAccount">登录账号</param>
/// <param name="Email">邮箱</param>
/// <param name="Phone">电话</param>
public record CreateDedsiUserCommand(
    string UserName, 
    string LoginAccount, 
    string LoginPwd,
    string Email,
    string Phone) : DedsiCommand<string>;

/// <summary>
/// 创建用户命令处理器
/// </summary>
/// <param name="dedsiUserRepository">用户仓储</param>
public class CreateDedsiUserCommandHandler(IDedsiUserRepository dedsiUserRepository) : DedsiCommandHandler<CreateDedsiUserCommand, string>
{
    public override async Task<string> Handle(CreateDedsiUserCommand command, CancellationToken cancellationToken)
    {
        var dedsiUser = new DedsiUser(
            Ulid.NewUlid().ToString(),
            command.UserName,
            command.LoginAccount,
            command.LoginPwd,
            command.Email,
            command.Phone
        );

        await dedsiUserRepository.InsertAsync(dedsiUser, false, cancellationToken);

        return dedsiUser.Id;
    }
}