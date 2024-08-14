using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Queries;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="dedsiMediator"></param>
/// <param name="userQuery"></param>
public class UserController(IDedsiMediator dedsiMediator, IUserQuery userQuery) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Guid> CreateUserAsync(CreateUserCommand command)
    {
        return dedsiMediator.PublishAsync(command);
    }

    /// <summary>
    /// 设置用户角色
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> SetUserRoleAsync(SetUserRoleCommand command)
    {
        await dedsiMediator.PublishAsync(command);
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