using ApiGateway.AgClusterConfigs;
using ApiGateway.AgRouteConfigs.CommamdHandlers;
using ApiGateway.Queries;
using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.AgRouteConfigs;

/// <summary>
/// AgRouteConfig
/// </summary>
/// <param name="agRouteConfigQuery"></param>
/// <param name="dedsiMediator"></param>
public class AgRouteConfigController(
    IAgRouteConfigQuery agRouteConfigQuery,
    IDedsiMediator dedsiMediator) : ApiGatewayController
{
    [HttpPost]
    public Task<bool> CreateAsync(AgRouteConfigRequestDto request)
    {
        return dedsiMediator.SendAsync(new CreateAgRouteConfigCommamd(request.RouteId, request.ClusterId, request.Match), HttpContext.RequestAborted);
    }

    [HttpPost]
    public Task<bool> DeleteByRouteIdAsync(DeleteByRouteIdRequestDto request)
    {
        return dedsiMediator.SendAsync(new DeleteByRouteIdCommamd(request.RouteId), HttpContext.RequestAborted);
    }

    [HttpPost]
    public async Task<GetPagedResponseDto> GetPagedAsync(GetPagedRequestDto request)
    {
        var result = await agRouteConfigQuery.GetPagedAsync(request.pageIndex, request.pageSize, request.RouteId, HttpContext.RequestAborted);

        var items = result.Item2.Select(a => new AgRouteConfigResponseDto
        {
            RouteId = a.RouteId,
            ClusterId = a.ClusterId,
            Match = new AgRouteMatchResponseDto
            {
                RouteId = a.Match.RouteId,
                Path = a.Match.Path
            }
        }).ToArray();

        return new GetPagedResponseDto(result.Item1, items);
    }
}

public record DeleteByRouteIdRequestDto(string RouteId);

public record GetPagedRequestDto(int pageIndex, int pageSize, string? RouteId);

public record GetPagedResponseDto(int TotalCount, AgRouteConfigResponseDto[] Items);
