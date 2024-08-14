using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Queries;
using Volo.Abp.EventBus.Local;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="localEventBus"></param>
/// <param name="userQuery"></param>
public class UserController(ILocalEventBus localEventBus, IUserQuery userQuery) : DedsiIdentityController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> CreateUserAsync(CreateUserCommand command)
    {
        await localEventBus.PublishAsync(command);
        return true;
    }

    /// <summary>
    /// 设置用户角色
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> SetUserRoleAsync(SetUserRoleCommand command)
    {
        await localEventBus.PublishAsync(command);
        return true;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<User?> GetAsync(Guid id)
    {
        return userQuery.GetByIdAsync(id);
    }
}