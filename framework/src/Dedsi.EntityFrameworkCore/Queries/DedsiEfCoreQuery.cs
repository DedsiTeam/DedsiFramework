using Dedsi.Ddd.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries;

/// <summary>
/// EF Core 实现的查询接口
/// </summary>
/// <param name="dbContextProvider"></param>
/// <typeparam name="TDbContext"></typeparam>
public class DedsiEfCoreQuery<TDbContext>(IDbContextProvider<TDbContext> dbContextProvider) : IDedsiQuery where TDbContext : IDedsiEfCoreDbContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual Task<TDbContext> GetDbContextAsync() => dbContextProvider.GetDbContextAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    protected virtual async Task<DbSet<TEntity>> GetDbSetAsync<TEntity>() where TEntity : class
    {
        return (await GetDbContextAsync()).Set<TEntity>();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    protected virtual async Task<IQueryable<TEntity>> GetNoTrackingQueryable<TEntity>() where TEntity : class
    {
        return (await GetDbSetAsync<TEntity>()).AsNoTracking();
    }
}