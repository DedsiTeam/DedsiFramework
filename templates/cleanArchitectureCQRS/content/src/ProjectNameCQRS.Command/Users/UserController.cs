using Dedsi.Ddd.CQRS;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="dedsiMediator"></param>
/// <param name="userQuery"></param>
public class UserController(IDedsiMediator dedsiMediator,IUserQuery userQuery) : ProjectNameCQRSController
{
    [HttpPost]
    public async Task<Guid> CreateUserAsync(CreateUserInputDto input)
    {
        var cmd = new CreateUserCommand(input);
        var id = await dedsiMediator.Send(cmd);
        return id;
    }
    
    [HttpGet("{id}")]
    public Task<User> GetByIdAsync(Guid id)
    {
        return userQuery.GetByIdAsync(id);
    }
}