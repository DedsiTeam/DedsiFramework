using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Dedsi.Ddd.Domain.Repositories;

public interface IDedsiRepository : IRepository;

public interface IDedsiRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    Task<IQueryable<TEntity>> GetQueryableNoTrackingAsync();
    
    /// <summary>
    /// 分页查询：页码至少为 1，页大小为 1～1000，偏移量不得超过 int.MaxValue。
    /// </summary>
    /// <param name="wherePredicate"></param>
    /// <param name="orderPredicate"></param>
    /// <param name="isReverse"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TOrderKey"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">页码、页大小或偏移量超出范围。</exception>
    Task<(int, List<TEntity>)> GetPagedListAsync<TOrderKey>(
        Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, TOrderKey>> orderPredicate,
        bool isReverse = true,
        int pageIndex = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 按条件批量删除，遵循 ABP 软删除、审计和实体事件流程；返回匹配实体数。
    /// </summary>
    /// <param name="wherePredicate"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> DeleteManyAsync(Expression<Func<TEntity, bool>> wherePredicate, bool autoSave = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 直接物理删除并返回受影响行数。立即执行 SQL，保留查询过滤器，
    /// 不经过实体审计、事件或变更跟踪；参与当前数据库事务，不单独提交。
    /// </summary>
    Task<int> DeleteDirectWithCountAsync(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行sql，返回受影响行数
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
}
