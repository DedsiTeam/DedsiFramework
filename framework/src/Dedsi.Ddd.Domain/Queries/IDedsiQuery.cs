#nullable enable
using System.Linq.Expressions;
using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.Domain.Queries;

/// <summary>
/// Query
/// </summary>
public interface IDedsiQuery : ITransientDependency;


public interface IDedsiEfCoreQuery : IDedsiQuery
{
    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        where TEntity : class;

    Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        where TEntity : class;
    
    Task<int> CountAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

    Task<TEntity> GetAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity?> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;
}