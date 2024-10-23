using Dedsi.Ddd.CQRS.CommandHandlers;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Queries;

namespace ProjectNameCQRS.Users.CommandHandlers;

public class DeleteUserCommandHandler(IUserRepository userRepository, IUserQuery userQuery) : DedsiCommandHandler<DeleteUserCommand, bool>
{
    public override async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await userRepository.DeleteAsync(a => a.Id == command.id, cancellationToken: cancellationToken);
        return true;
    }
}
