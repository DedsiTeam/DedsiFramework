using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries;

public class DedsiEfCoreQuery<TDbContext>(IDbContextProvider<TDbContext> dbContextProvider) : IDedsiQuery
    where TDbContext : IDedsiEfCoreDbContext
{
    protected virtual Task<TDbContext> GetDbContextAsync() => dbContextProvider.GetDbContextAsync();

    protected async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }
    
    protected async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.Set<TEntity>().CountAsync(predicate, cancellationToken);
    }
    
}