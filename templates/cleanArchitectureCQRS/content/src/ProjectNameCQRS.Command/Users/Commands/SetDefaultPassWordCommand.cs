using Dedsi.Ddd.CQRS;

namespace ProjectNameCQRS.Users.Commands
{
    public record SetDefaultPassWordCommand(Guid Id) : IDedsiCommand<string>;
}
