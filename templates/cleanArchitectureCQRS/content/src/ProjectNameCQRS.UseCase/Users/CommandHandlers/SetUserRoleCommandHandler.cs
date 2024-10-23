using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using Volo.Abp;
using Volo.Abp.Guids;

namespace ProjectNameCQRS.Users.CommandHandlers;

public class SetUserRoleCommandHandler(
    IUserRepository userRepository,
    IGuidGenerator guidGenerator)
    : DedsiCommandHandler<SetUserRoleCommand>
{
    public override async Task Handle(SetUserRoleCommand eventData, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(a => a.Id == eventData.userId, cancellationToken: cancellationToken);

        foreach (var role in eventData.Roles)
        {
            user.SetUserRole(new UserRole(guidGenerator.Create(), user.Id, role.Id, role.RoleName));
        }
    }
}
