using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Repositories;

public class DedsiCqrsEfCoreRepository<TDbContext, TEntity, TKey>(IDbContextProvider<TDbContext> dbContextProvider)
    : EfCoreRepository<TDbContext, TEntity, TKey>(dbContextProvider),
        IDedsiCqrsRepository<TEntity, TKey>
    where TDbContext : IDedsiEfCoreDbContext
    where TEntity : class, IEntity<TKey>
{
    
    /// <inheritdoc />
    public async Task<int> DeleteManyAsync(Expression<Func<TEntity, bool>> wherePredicate, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        
        var count = await (await GetQueryableAsync()).Where(wherePredicate).ExecuteDeleteAsync(cancellationToken);

        if (autoSave)
        {
            await SaveChangesAsync(cancellationToken);
        }

        return count;
    }
    
}