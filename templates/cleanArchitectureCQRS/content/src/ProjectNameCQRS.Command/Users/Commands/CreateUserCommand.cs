using Dedsi.Ddd.CQRS;
using ProjectNameCQRS.Users.Dtos;

namespace ProjectNameCQRS.Users.Commands;

/// <summary>
/// 命令
/// </summary>
/// <param name="UserDto"></param>
public record CreateUserCommand(CreateUserInputDto UserDto) : IDedsiCommand<Guid>;