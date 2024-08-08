using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Repositories.Users;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;
using Volo.Abp.EventBus.Local;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="localEventBus"></param>
/// <param name="userQuery"></param>
public class UserCQRSController(ILocalEventBus localEventBus,IUserQuery userQuery) : ProjectNameCQRSController
{
    [HttpPost]
    public async Task<bool> CreateUserAsync(CreateUserInputDto input)
    {
        var cmd = new CreateUserCommand(input);
        await localEventBus.PublishAsync(cmd);
        return true;
    }
    
    [HttpGet("{id}")]
    public Task<User> GetByIdAsync(Guid id)
    {
        return userQuery.GetByIdAsync(id);
    }
}