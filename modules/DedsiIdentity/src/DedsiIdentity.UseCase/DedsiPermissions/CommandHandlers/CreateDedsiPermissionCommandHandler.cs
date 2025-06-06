using Dedsi.Ddd.CQRS.CommandHandlers;
using Dedsi.Ddd.CQRS.Commands;
using DedsiIdentity.Repositories.DedsiPermissions;

namespace DedsiIdentity.DedsiPermissions.CommandHandlers;

/// <summary>
/// 
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="GroupName"></param>
/// <param name="GroupCode"></param>
public record CreateDedsiPermissionCommand(string Name, string Description, string GroupName, string GroupCode) : DedsiCommand<string>;

/// <summary>
/// 
/// </summary>
/// <param name="dedsiPermissionRepository"></param>
public class CreateDedsiPermissionCommandHandler(IDedsiPermissionRepository dedsiPermissionRepository) : DedsiCommandHandler<CreateDedsiPermissionCommand, string>
{
    public override async Task<string> Handle(CreateDedsiPermissionCommand command, CancellationToken cancellationToken)
    {
        var dedsiPermission = new DedsiPermission(
            Ulid.NewUlid().ToString(), 
            command.Name, 
            command.Description, 
            command.GroupName, 
            command.GroupCode);

        await dedsiPermissionRepository.InsertAsync(dedsiPermission, false, cancellationToken);

        return dedsiPermission.Id;
    }
}
