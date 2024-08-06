using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Dedsi.Ddd.Domain.Repositories;

public interface IDedsiRepository<TEntity,TKey> : IRepository<TEntity,TKey> where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// 启用：IsChangeTrackingEnabled
    /// </summary>
    void EnableChangeTracking();
    
    /// <summary>
    /// 关闭：IsChangeTrackingEnabled
    /// </summary>
    void CloseChangeTracking();
}