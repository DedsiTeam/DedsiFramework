#nullable enable
using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries;

public class DedsiEfCoreQuery<TDbContext>(IDbContextProvider<TDbContext> dbContextProvider) : IDedsiEfCoreQuery
    where TDbContext : IDedsiEfCoreDbContext
{
    protected virtual Task<TDbContext> GetDbContextAsync() => dbContextProvider.GetDbContextAsync();

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }
    
    public async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().CountAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();
        
        return await dbContext.Set<TEntity>().CountAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class
    {
        var entity = await FindAsync<TEntity, TKey>(id, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public async Task<TEntity?> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().FindAsync([ id! ], cancellationToken);
    }

    public async Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
    {
        var entity = await FindAsync(predicate, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }

        return entity;
    }
}