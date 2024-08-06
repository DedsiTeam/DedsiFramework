using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;

namespace Dedsi.EntityFrameworkCore.Repositories;

public class EfCoreRepository<TEntity>: EfCoreReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class, new()
{
    public Task<bool> InsertAsync(List<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> InsertReturnIdentityAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateColumnsAsync(TEntity entity, Expression<Func<TEntity, object>> columns)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IgnoreColumnsAsync(TEntity entity, Expression<Func<TEntity, object>> columns)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(List<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateColumnsAsync(List<TEntity> entities, Expression<Func<TEntity, object>> columns)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IgnoreColumnsAsync(List<TEntity> entities, Expression<Func<TEntity, object>> columns)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync<TPrimaryKey>(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync<TPrimaryKey>(IEnumerable<TPrimaryKey> ids)
    {
        throw new NotImplementedException();
    }

    public Task<int> ExecuteCommandAsync(string sql, object parameters)
    {
        throw new NotImplementedException();
    }
}