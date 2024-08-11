using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;
using ProjectNameCQRS.Users.Queries;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="dedsiMediator"></param>
public class UserController(IDedsiMediator dedsiMediator,IUserQuery userQuery) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Guid> CreateUserAsync(CreateUserCommand command)
    {
        return dedsiMediator.Send(command);
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand command)
    {
        return userQuery.SearchUserAsync(command);
    }
    
}