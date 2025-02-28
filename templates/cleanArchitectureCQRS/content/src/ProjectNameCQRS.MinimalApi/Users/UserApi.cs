using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProjectNameCQRS.Users.CommandHandlers;
using ProjectNameCQRS.Users.Queries;
using ProjectNameCQRS.Users.Requests;

namespace ProjectNameCQRS.UserApis;

public static class UserApi
{
    public static void MapUserApis(this IEndpointRouteBuilder builder)
    {
        var api = builder
            .MapGroup(ProjectNameCQRSMinimalApi.ApiPrefix + "user")
            .WithTags("User");

        api
            .MapGet("Get/{id:guid}", (Guid id, IUserQuery userQuery) => userQuery.GetByidAsync(id))
            .WithSummary("获取用户信息");
        
        api.MapPost("/Create", CreateAsync);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dedsiMediator"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static Task CreateAsync([FromBody] CreateUserRequest input, IDedsiMediator dedsiMediator, HttpContext httpContext)
    {
        return dedsiMediator.SendAsync(new CreateUserCommand(input.UserName, input.Account, input.Email));
    }
}