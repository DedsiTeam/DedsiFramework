using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Repositories;

public abstract class DedsiEfCoreRepository<TDbContext, TEntity>(IDbContextProvider<TDbContext> dbContextProvider)
    : EfCoreRepository<TDbContext,TEntity>(dbContextProvider), IDedsiRepository<TEntity>
    where TDbContext : IDedsiEfCoreDbContext
    where TEntity : class, IEntity
{
    /// <inheritdoc />
    public virtual void EnableChangeTracking()
    {
        this.IsChangeTrackingEnabled = true;
    }

    /// <inheritdoc />
    public virtual void CloseChangeTracking()
    {
        this.IsChangeTrackingEnabled = false;
    }
    
    /// <inheritdoc />
    public virtual async Task<(int,List<TEntity>)> GetPagedListAsync<TOrderKey>(
        Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, TOrderKey>> orderPredicate,
        bool isReverse = true, 
        int pageIndex = 1,
        int pageSize = 10)
    {
        var whereQueryable = (await GetQueryableAsync()).Where(wherePredicate);
        // 数量
        var dbCount = await whereQueryable.CountAsync();
        // 排序
        whereQueryable = isReverse ? whereQueryable.OrderByDescending(orderPredicate) : whereQueryable.OrderBy(orderPredicate);

        var dbList = await whereQueryable
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (dbCount, dbList);
    }

    /// <inheritdoc />
    public virtual async Task<int> DeleteManyAsync(Expression<Func<TEntity, bool>> wherePredicate)
    {
        return await (await GetQueryableAsync()).Where(wherePredicate).ExecuteDeleteAsync();
    }

    /// <inheritdoc />
    public virtual async Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await (await GetDbContextAsync()).Database.ExecuteSqlAsync(sql,cancellationToken);
    }
}

public abstract class DedsiEfCoreRepository<TDbContext,TEntity, TKey>(IDbContextProvider<TDbContext> dbContextProvider) 
    : DedsiEfCoreRepository<TDbContext,TEntity>(dbContextProvider), IDedsiRepository<TEntity, TKey>
    where TDbContext : IDedsiEfCoreDbContext
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// abp EfCoreRepository
    /// </summary>
    private readonly EfCoreRepository<TDbContext, TEntity, TKey> _efCoreRepository = new (dbContextProvider);
    
    /// <inheritdoc />
    public virtual Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return _efCoreRepository.GetAsync(id, includeDetails, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return _efCoreRepository.GetAsync(id, includeDetails, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        return _efCoreRepository.DeleteAsync(id, autoSave, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        return _efCoreRepository.DeleteManyAsync(ids, autoSave, cancellationToken);
    }
}