using Microsoft.AspNetCore.Routing;
using ProjectNameCQRS.UserApis;

namespace ProjectNameCQRS;

public static class ProjectNameCQRSMinimalApi
{
    public const string ApiPrefix = "api/ProjectNameCQRS";
    
    /// <summary>
    /// Map ProjectNameCQRS MinimalApis
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapProjectNameCQRSMinimalApis(this IEndpointRouteBuilder builder)
    {
        builder.MapUserApis();

        return builder;
    }
}