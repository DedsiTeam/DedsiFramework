using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiUsers;

namespace DedsiIdentity.DedsiUsers.CommandHandlers;

/// <summary>
/// 更新用户命令
/// </summary>
/// <param name="Id">用户ID</param>
/// <param name="UserName">用户名</param>
/// <param name="Email">邮箱</param>
/// <param name="Phone">电话</param>
public record UpdateDedsiUserCommand(
    string Id,
    string UserName,
    string Email,
    string Phone) : DedsiCommand<bool>;

/// <summary>
/// 更新用户命令处理器
/// </summary>
/// <param name="dedsiUserRepository">用户仓储</param>
public class UpdateDedsiUserCommandHandler(IDedsiUserRepository dedsiUserRepository) : DedsiCommandHandler<UpdateDedsiUserCommand, bool>
{
    public override async Task<bool> Handle(UpdateDedsiUserCommand command, CancellationToken cancellationToken)
    {
        var dedsiUser = await dedsiUserRepository.GetAsync(a => a.Id == command.Id, true, cancellationToken);

        if (dedsiUser == null)
        {
            return false;
        }

        dedsiUser.ChangeUserName(command.UserName);
        dedsiUser.ChangeEmail(command.Email);
        dedsiUser.ChangePhone(command.Phone);

        await dedsiUserRepository.UpdateAsync(dedsiUser, false, cancellationToken);

        return true;
    }
}