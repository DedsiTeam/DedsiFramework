using Dedsi.Ddd.Domain.Repositories;
using Dedsi.Core.Paging;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Repositories;

public abstract class DedsiEfCoreRepository<TDbContext,TEntity, TKey>(IDbContextProvider<TDbContext> dbContextProvider) 
    : EfCoreRepository<TDbContext,TEntity, TKey>(dbContextProvider), IDedsiRepository<TEntity, TKey>
    where TDbContext : IDedsiEfCoreDbContext
    where TEntity : class, IEntity<TKey>
{
    /// <inheritdoc />
    public async Task<IQueryable<TEntity>> GetQueryableNoTrackingAsync()
    {
        return (await GetDbSetAsync()).AsNoTracking();
    }

    /// <inheritdoc />
    public virtual async Task<(int,List<TEntity>)> GetPagedListAsync<TOrderKey>(
        Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, TOrderKey>> orderPredicate,
        bool isReverse = true, 
        int pageIndex = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var skipCount = DedsiPaging.GetSkipCount(pageIndex, pageSize);
        cancellationToken = GetCancellationToken(cancellationToken);
        
        var whereQueryable = (await GetQueryableAsync()).Where(wherePredicate);
        // 数量
        var dbCount = await whereQueryable.CountAsync(cancellationToken);
        // 排序
        whereQueryable = isReverse ? whereQueryable.OrderByDescending(orderPredicate) : whereQueryable.OrderBy(orderPredicate);

        var dbList = await whereQueryable
            .Skip(skipCount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (dbCount, dbList);
    }

    /// <inheritdoc />
    public virtual async Task<int> DeleteManyAsync(Expression<Func<TEntity, bool>> wherePredicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        
        var entities = await (await GetQueryableAsync()).Where(wherePredicate).ToListAsync(cancellationToken);
        await base.DeleteManyAsync(entities, autoSave, cancellationToken);
        return entities.Count;
    }

    /// <inheritdoc />
    public virtual async Task<int> DeleteDirectWithCountAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync()).Where(wherePredicate).ExecuteDeleteAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        
        return await (await GetDbContextAsync()).Database.ExecuteSqlAsync(sql, cancellationToken);
    }
    
}