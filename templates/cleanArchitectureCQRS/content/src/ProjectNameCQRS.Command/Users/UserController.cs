using Dedsi.Ddd.CQRS;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Users.Commands;

namespace ProjectNameCQRS.Users;

/// <summary>
/// 用户
/// </summary>
/// <param name="dedsiMediator"></param>
/// <param name="userQuery"></param>
public class UserController(IDedsiMediator dedsiMediator) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<Guid> CreateUserAsync(CreateUserCommand command)
    {
        var id = await dedsiMediator.Send(command);
        return id;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<SearchUserPagedResultDto> SearchUserAsync(SearchUserCommand command)
    {
        var result = await dedsiMediator.Send(command);
        return result;
    }
    
}