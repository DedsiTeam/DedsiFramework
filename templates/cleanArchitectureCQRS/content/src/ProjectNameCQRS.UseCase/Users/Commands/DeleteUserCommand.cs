using Dedsi.Ddd.CQRS.Commands;

namespace ProjectNameCQRS.Users.Commands;

public record DeleteUserCommand(Guid id) : DedsiCommand<bool>;
