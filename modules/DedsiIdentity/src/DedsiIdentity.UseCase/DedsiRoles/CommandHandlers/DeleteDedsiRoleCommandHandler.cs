using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiRoles;

namespace DedsiIdentity.DedsiRoles.CommandHandlers;

public record DeleteDedsiRoleCommand(string DedsiRoleId) : DedsiCommand<bool>;

public class DeleteDedsiRoleCommandHandler(IDedsiRoleRepository dedsiRoleRepository) : DedsiCommandHandler<DeleteDedsiRoleCommand, bool>
{
    public override async Task<bool> Handle(DeleteDedsiRoleCommand command, CancellationToken cancellationToken)
    {
        var dedsiRole = await dedsiRoleRepository.GetAsync(a => a.Id == command.DedsiRoleId, true, cancellationToken);

        await dedsiRoleRepository.DeleteAsync(dedsiRole, false, cancellationToken);

        return true;
    }
}