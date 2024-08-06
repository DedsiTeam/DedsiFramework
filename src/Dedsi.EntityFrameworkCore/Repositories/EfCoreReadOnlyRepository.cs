using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;

namespace Dedsi.EntityFrameworkCore.Repositories;

public class EfCoreReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync(string sql, object whereObj)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> GetAsync<TResult>(string sql, object whereObj)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetListAsync(string sql, object whereObj)
    {
        throw new NotImplementedException();
    }

    public Task<List<TResult>> GetListAsync<TResult>(string sql, object whereObj)
    {
        throw new NotImplementedException();
    }

    public Task<(int, List<TEntity>)> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderExpression,
        OrderByTypeEnum orderByType = OrderByTypeEnum.Desc)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(string sql, object whereObj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        throw new NotImplementedException();
    }
}