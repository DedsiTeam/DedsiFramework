using Dedsi.Ddd.CQRS.Commands;

namespace ProjectNameCQRS.Roles.Commands;

public record CreateRoleCommand(string RoleName, string RoleCode) : DedsiCommand<Guid>;
