using Dedsi.Ddd.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
    public async Task<IQueryable<TEntity>> GetQueryableNoTrackingAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet.AsNoTracking();
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
    /// <inheritdoc />
    public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        return includeDetails
            ? await (await WithDetailsAsync()).OrderBy(e => e.Id).FirstOrDefaultAsync(e => e.Id!.Equals(id), GetCancellationToken(cancellationToken))
            : !ShouldTrackingEntityChange()
                ? await (await GetQueryableAsync()).OrderBy(e => e.Id).FirstOrDefaultAsync(e => e.Id!.Equals(id), GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).FindAsync(new object[] { id! }, GetCancellationToken(cancellationToken));
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        var entity = await FindAsync(id, cancellationToken: cancellationToken);
        if (entity == null)
        {
            return;
        }

        await DeleteAsync(entity, autoSave, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var entities = await (await GetDbSetAsync()).Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

        await DeleteManyAsync(entities, autoSave, cancellationToken);
    }
}