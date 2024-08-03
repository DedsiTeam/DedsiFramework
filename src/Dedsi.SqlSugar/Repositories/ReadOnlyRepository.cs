using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using SqlSugar;

namespace Dedsi.SqlSugar.Repositories;

public class ReadOnlyRepository<TEntity>(ISqlSugarClient sqlSugarClient) : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <inheritdoc />
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return sqlSugarClient.Queryable<TEntity>().FirstAsync(whereExpression);
    }

    /// <inheritdoc />
    public Task<TEntity> GetAsync(string sql, object whereObj)
    {
        return GetAsync<TEntity>(sql, whereObj);
    }

    /// <inheritdoc />
    public Task<TResult> GetAsync<TResult>(string sql, object whereObj)
    {
        return sqlSugarClient.Ado.SqlQuerySingleAsync<TResult>(sql,whereObj);
    }

    /// <inheritdoc />
    public Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id)
    {
        return sqlSugarClient.Queryable<TEntity>().In(id).FirstAsync();
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return sqlSugarClient.Queryable<TEntity>().Where(whereExpression).ToListAsync();
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync(string sql, object whereObj)
    {
        return GetListAsync<TEntity>(sql, whereObj);
    }

    /// <inheritdoc />
    public Task<List<TResult>> GetListAsync<TResult>(string sql, object whereObj)
    {
        return sqlSugarClient.Ado.SqlQueryAsync<TResult>(sql,whereObj);
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync()
    {
        return sqlSugarClient.Queryable<TEntity>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<(int, List<TEntity>)> GetPagedListAsync(
        int pageNumber, 
        int pageSize,
        Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<TEntity, object>> orderExpression, 
        OrderByType orderByType = OrderByType.Desc)
    {
        RefAsync<int> totalNumber = 0;
        var dbList = await sqlSugarClient
                            .Queryable<TEntity>()
                            .Where(whereExpression)
                            .OrderBy(orderExpression, orderByType)
                            .ToPageListAsync(pageNumber,pageSize,totalNumber);

        return (totalNumber,dbList);
    }
    
    /// <inheritdoc />
    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return sqlSugarClient.Queryable<TEntity>().CountAsync(whereExpression);
    }
    
    /// <inheritdoc />
    public Task<int> GetCountAsync()
    {
        return sqlSugarClient.Queryable<TEntity>().CountAsync();
    }

    public Task<int> CountAsync(string sql, object whereObj)
    {
        return sqlSugarClient.Ado.SqlQuerySingleAsync<int>(sql,whereObj);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return sqlSugarClient.Queryable<TEntity>().AnyAsync(whereExpression);
    }
}