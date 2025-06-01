using ApiGateway.AgClusterConfigs;
using ApiGateway.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.DependencyInjection;

namespace ApiGateway.Queries;

public interface IAgClusterConfigQuery : ITransientDependency
{
    Task<string[]> GetAllClusterIdAsync(CancellationToken cancellationToken);


    Task<AgClusterConfig[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(int, AgClusterConfig[])> GetPagedAsync(int pageIndex, int pageSize, string? clusterId, CancellationToken cancellationToken = default);
}

public class AgClusterConfigQuery(ApiGatewayDbContext apiGatewayDbContext) : IAgClusterConfigQuery
{
    /// <inheritdoc/>
    public async Task<AgClusterConfig[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await apiGatewayDbContext.AgClusterConfigs
            .Include(a => a.Destinations)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<string[]> GetAllClusterIdAsync(CancellationToken cancellationToken)
    {
        return await apiGatewayDbContext.AgClusterConfigs.Select(a => a.ClusterId).ToArrayAsync();
    }

    /// <inheritdoc/>
    public async Task<(int, AgClusterConfig[])> GetPagedAsync(int pageIndex, int pageSize, string? clusterId, CancellationToken cancellationToken = default)
    {
        var query = apiGatewayDbContext.AgClusterConfigs
            .Include(a => a.Destinations)
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(clusterId), a => a.ClusterId.Contains(clusterId!));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(a => a.ClusterId)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);

        return (totalCount, items);
    }
}
