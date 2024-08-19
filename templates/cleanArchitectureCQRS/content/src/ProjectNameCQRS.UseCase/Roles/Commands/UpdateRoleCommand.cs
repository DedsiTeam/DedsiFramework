using Dedsi.Ddd.CQRS.Commands;

namespace ProjectNameCQRS.Roles.Commands;

public record UpdateRoleCommand(Guid id,string RoleName, string RoleCode) : DedsiCommand<bool>;
