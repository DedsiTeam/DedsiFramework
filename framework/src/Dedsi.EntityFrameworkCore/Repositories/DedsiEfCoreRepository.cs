using Dedsi.Ddd.Domain.Repositories;
using Dedsi.EntityFrameworkCore.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dedsi.EntityFrameworkCore.Repositories;


public abstract class DedsiEfCoreRepository<TDbContext,TEntity, TKey>(IDbContextProvider<TDbContext> dbContextProvider) 
    : EfCoreRepository<TDbContext,TEntity, TKey>(dbContextProvider),
        IDedsiRepository<TEntity, TKey>
    where TDbContext : IDedsiEfCoreDbContext
    where TEntity : class, IEntity<TKey>
{
    /// <inheritdoc />
    public void EnableChangeTracking()
    {
        this.IsChangeTrackingEnabled = true;
    }

    /// <inheritdoc />
    public void CloseChangeTracking()
    {
        this.IsChangeTrackingEnabled = false;
    }
}