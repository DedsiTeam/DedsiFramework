using Dedsi.Ddd.CQRS.Commands;

namespace ProjectNameCQRS.Roles.Commands;

public record DeleteRoleCommand(Guid id) : DedsiCommand<bool>;
