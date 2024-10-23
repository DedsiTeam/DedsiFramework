using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Roles;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IGuidGenerator guidGenerator)
    : DedsiCommandHandler<CreateUserCommand, Guid>
{
    public override async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // 普通用户角色
        var role = await roleRepository.GetAsync(a => a.RoleCode == "OrdinaryUser", cancellationToken: cancellationToken);

        // 创建用户
        var user = new User(guidGenerator.Create(), command.UserName, command.Account, "PassWord@" + DateTime.Now.Year, command.Email, role);

        // 保存到数据库
        await userRepository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id;
    }
}