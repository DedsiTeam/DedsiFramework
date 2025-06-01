using ApiGateway.AgClusterConfigs.CommamdHandlers;
using ApiGateway.Queries;
using Dedsi.Ddd.CQRS.Mediators;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.AgClusterConfigs;

/// <summary>
/// AgClusterConfig
/// </summary>
/// <param name="agClusterConfigQuery"></param>
/// <param name="dedsiMediator"></param>
public class AgClusterConfigController(
    IAgClusterConfigQuery agClusterConfigQuery,
    IDedsiMediator dedsiMediator) : ApiGatewayController
{
    [HttpPost]
    public Task<bool> CreateAsync(AgClusterConfigRequestDto request)
    {
        return dedsiMediator.SendAsync(new CreateAgClusterConfigCommamd(request.ClusterId, request.Destinations), HttpContext.RequestAborted);
    }

    [HttpGet]
    public Task<string[]> GetAllClusterIdAsync()
    {
        return agClusterConfigQuery.GetAllClusterIdAsync(HttpContext.RequestAborted);
    }

    [HttpPost]
    public Task<bool> DeleteByClusterIdAsync(DeleteByClusterIdRequestDto request)
    {
        return dedsiMediator.SendAsync(new DeleteByClusterIdCommamd(request.ClusterId), HttpContext.RequestAborted);
    }

    [HttpPost]
    public async Task<GetPagedResponseDto> GetPagedAsync(GetPagedRequestDto request)
    {
        var result = await agClusterConfigQuery.GetPagedAsync(request.pageIndex, request.pageSize, request.ClusterId, HttpContext.RequestAborted);
    
        var items = result.Item2.Select(a => new AgClusterConfigResponseDto()
        {
            ClusterId = a.ClusterId,
            Destinations = a.Destinations.Select(d => new AgClusterDestinationConfigResponseDto
            {
                DestinationId = d.DestinationId,
                Address = d.Address
            }).ToList()
        }).ToArray();

        return new GetPagedResponseDto(result.Item1, items);
    }
}

public record DeleteByClusterIdRequestDto(string ClusterId);

public record GetPagedRequestDto(int pageIndex, int pageSize, string? ClusterId);

public record GetPagedResponseDto(int TotalCount, AgClusterConfigResponseDto[] Items);
