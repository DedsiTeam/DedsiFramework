using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Queries;
using Dedsi.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Queries;

public class DedsiEfCoreQuery<TDbContext,TEntity,TKey>(IDbContextProvider<TDbContext> dbContextProvider) 
    : IDedsiQuery<TEntity,TKey>
    where TEntity : class
    where TDbContext : IDedsiEfCoreDbContext
{
    protected virtual Task<TDbContext> GetDbContextAsync() => dbContextProvider.GetDbContextAsync();

    protected virtual async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        var dbContext = await GetDbContextAsync();
        return dbContext.Set<TEntity>();
    }
    
    public virtual async Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> wherePredicate)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(wherePredicate).FirstOrDefaultAsync();
    }

    public virtual async Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> wherePredicate)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(wherePredicate).ToListAsync();
    }
}