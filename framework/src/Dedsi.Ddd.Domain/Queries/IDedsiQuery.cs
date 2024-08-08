using System.Linq.Expressions;
using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.Domain.Queries;

/// <summary>
/// 查询
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IDedsiQuery<TEntity, in TKey> : ITransientDependency
{
    Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> wherePredicate);

    Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> wherePredicate);
}