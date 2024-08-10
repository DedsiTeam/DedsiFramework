using Dedsi.Ddd.CQRS;

namespace ProjectNameCQRS.Users.Commands;

public record CreateUserCommand(string UserName,string Account,string Email) : IDedsiCommand<Guid>;