using ApiGateway.AgRouteConfigs;
using ApiGateway.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace ApiGateway.Queries;

public interface IAgRouteConfigQuery: ITransientDependency
{
    Task<AgRouteConfig[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(int, AgRouteConfig[])> GetPagedAsync(int pageIndex, int pageSize, string? routeId, CancellationToken cancellationToken);
}


public class AgRouteConfigQuery(ApiGatewayDbContext apiGatewayDbContext) : IAgRouteConfigQuery
{
    public async Task<AgRouteConfig[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await apiGatewayDbContext.AgRouteConfigs
            .Include(a => a.Match)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<(int, AgRouteConfig[])> GetPagedAsync(int pageIndex, int pageSize, string? routeId, CancellationToken cancellationToken = default)
    {
        var query = apiGatewayDbContext.AgRouteConfigs
            .Include(a => a.Match)
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(routeId), a => a.RouteId.Contains(routeId!));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(a => a.RouteId)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return (totalCount, items);
    }
}
