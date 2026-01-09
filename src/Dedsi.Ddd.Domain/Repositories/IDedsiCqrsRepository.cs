using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;

namespace Dedsi.Ddd.Domain.Repositories;

/// <summary>
/// CRQS 仓储
/// </summary>
/// <typeparam name="TDomain"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IDedsiCqrsRepository<TDomain, in TKey> where TDomain : class, IEntity<TKey>
{
    /// <summary>
    /// 插入一个
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomain> InsertAsync(TDomain domain, bool autoSave = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomain> UpdateAsync(TDomain domain, bool autoSave = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除单个
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TDomain domain, bool autoSave = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="wherePredicate"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TDomain, bool>> wherePredicate, bool autoSave = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 单个查询
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomain> GetAsync(Expression<Func<TDomain, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 通过主键Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TDomain> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
}