using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Queries;

namespace ProjectNameCQRS.Users.CommandHandlers;

public class DeleteUserCommandHandler(IUserRepository userRepository, IUserQuery userQuery) : DedsiCommandHandler<DeleteUserCommand, bool>
{
    public override async Task<bool> HandleEventAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userQuery.GetByIdAsync(command.id);
        await userRepository.DeleteAsync(user);
        return true;
    }
}
