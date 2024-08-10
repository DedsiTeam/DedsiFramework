using Dedsi.Ddd.CQRS.Commands;

namespace ProjectNameCQRS.Users.Commands;

public record CreateUserCommand(string UserName,string Account,string Email) : DedsiCommand<Guid>;