using System.Linq.Expressions;
using Mapster;

namespace Dedsi.Ddd.Domain.Queries;

public static class DedsiEfCoreQueryExtensions
{
    public static async Task<TDto> GetDtoAsync<TEntity, TKey, TDto>(this IDedsiEfCoreQuery query,TKey id, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var entity = await query.GetAsync<TEntity, TKey>(id, cancellationToken);

        return entity.Adapt<TDto>();
    }

    public static async Task<TDto> GetDtoAsync<TEntity, TDto>(this IDedsiEfCoreQuery query,Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
    {
        var entity = await query.GetAsync(predicate, cancellationToken);

        return entity.Adapt<TDto>();
    }
}