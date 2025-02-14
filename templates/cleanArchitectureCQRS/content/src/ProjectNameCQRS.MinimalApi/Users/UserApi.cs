using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProjectNameCQRS.Users.Commands;
using ProjectNameCQRS.Users.Dtos;

namespace ProjectNameCQRS.UserApis;

public static class UserApi
{
    public static void MapUserApis(this IEndpointRouteBuilder builder)
    {
        var api = builder
            .MapGroup(ProjectNameCQRSMinimalApi.ApiPrefix + "user")
            .WithTags("User");

        api.MapPost("/Create", CreateAsync);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dedsiMediator"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static Task CreateAsync([FromBody] CreateUserInputDto input, IDedsiMediator dedsiMediator, HttpContext httpContext)
    {
        return dedsiMediator.SendAsync(new CreateUserCommand(input.UserName, input.Account, input.Email));
    }
}