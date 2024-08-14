namespace ProjectNameCQRS.Users.Commands;

/// <summary>
/// 命令：创建用户
/// </summary>
/// <param name="UserName"></param>
/// <param name="Account"></param>
/// <param name="Email"></param>
public record CreateUserCommand(string UserName, string Account, string Email);