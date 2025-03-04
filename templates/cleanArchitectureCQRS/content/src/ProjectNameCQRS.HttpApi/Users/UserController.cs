using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;
using ProjectNameCQRS.Users.CommandHandlers;
using ProjectNameCQRS.Users.Queries;
using ProjectNameCQRS.Users.Requests;
using ProjectNameCQRS.Users.Responses;

namespace ProjectNameCQRS.Users;

public class UserController(IUserQuery userQuery,IDedsiMediator dedsiMediator) : ProjectNameCQRSController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Guid> CreateAsync(CreateUserRequest input)
    {
        return dedsiMediator.SendAsync(new CreateUserCommand(input.UserName, input.Account, input.Email), HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<UserInfoResponse> GetAsync(Guid id)
    {
        return userQuery.GetByidAsync(id, HttpContext.RequestAborted);
    }
}