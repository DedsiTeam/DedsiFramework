#nullable enable
using System.Linq.Expressions;

namespace Dedsi.Ddd.Domain.Queries;

/// <summary>
/// EF Core Query
/// </summary>
public interface IDedsiEfCoreQuery : IDedsiQuery
{
    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;

    Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;
    
    Task<int> CountAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

    Task<TEntity> GetAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity?> FindAsync<TEntity, TKey>(TKey id, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;

    Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class;
}