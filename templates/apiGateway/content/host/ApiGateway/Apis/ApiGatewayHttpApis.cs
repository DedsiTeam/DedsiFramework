using Dedsi.ApiGateway;
using Yarp.ReverseProxy.Configuration;

namespace ApiGateway.Apis;

public static class ApiGatewayHttpApis
{
    public static void MapApiGatewayHttpApis(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/gateway", (IProxyConfigProvider proxyConfigProvider) =>
        {
            if (proxyConfigProvider is ApiGatewayProxyConfigProvider)
            {
                ((ApiGatewayProxyConfigProvider)proxyConfigProvider).RefreshConfig();
                return Results.Ok("API Gateway configuration refreshed successfully.");
            }

            return Results.BadRequest("Invalid proxy config provider.");
        });
    }
}
