using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiUsers;

namespace DedsiIdentity.DedsiUsers.CommandHandlers;

public record DeleteDedsiUserCommand(string DedsiUserId) : DedsiCommand<bool>;

public class DeleteDedsiUserCommandHandler(IDedsiUserRepository dedsiUserRepository) : DedsiCommandHandler<DeleteDedsiUserCommand, bool>
{
    public override async Task<bool> Handle(DeleteDedsiUserCommand command, CancellationToken cancellationToken)
    {
        var dedsiUser = await dedsiUserRepository.GetAsync(a => a.Id == command.DedsiUserId, true, cancellationToken);

        await dedsiUserRepository.DeleteAsync(dedsiUser, false, cancellationToken);

        return true;
    }
}