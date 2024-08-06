using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Repositories;
using SqlSugar;

namespace Dedsi.SqlSugar.Repositories;

public class SqlSugarReadOnlyRepository<TEntity>(ISqlSugarClient readOnlyClient) : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <inheritdoc />
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return readOnlyClient.Queryable<TEntity>().FirstAsync(whereExpression);
    }

    /// <inheritdoc />
    public Task<TEntity> GetAsync(string sql, object whereObj)
    {
        return GetAsync<TEntity>(sql, whereObj);
    }

    /// <inheritdoc />
    public Task<TResult> GetAsync<TResult>(string sql, object whereObj)
    {
        return readOnlyClient.Ado.SqlQuerySingleAsync<TResult>(sql,whereObj);
    }

    /// <inheritdoc />
    public Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id)
    {
        return readOnlyClient.Queryable<TEntity>().In(id).FirstAsync();
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return readOnlyClient.Queryable<TEntity>().Where(whereExpression).ToListAsync();
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync(string sql, object whereObj)
    {
        return GetListAsync<TEntity>(sql, whereObj);
    }

    /// <inheritdoc />
    public Task<List<TResult>> GetListAsync<TResult>(string sql, object whereObj)
    {
        return readOnlyClient.Ado.SqlQueryAsync<TResult>(sql,whereObj);
    }

    /// <inheritdoc />
    public Task<List<TEntity>> GetListAsync()
    {
        return readOnlyClient.Queryable<TEntity>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<(int, List<TEntity>)> GetPagedListAsync(
        int pageNumber, 
        int pageSize,
        Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<TEntity, object>> orderExpression, 
        OrderByTypeEnum orderByType = OrderByTypeEnum.Desc)
    {
        OrderByType orderByTypeSqlSugar = OrderByType.Desc;
        if (orderByType == OrderByTypeEnum.Asc)
        {
            orderByTypeSqlSugar = OrderByType.Asc;
        }
        
        RefAsync<int> totalNumber = 0;
        var dbList = await readOnlyClient
                            .Queryable<TEntity>()
                            .Where(whereExpression)
                            .OrderBy(orderExpression, orderByTypeSqlSugar)
                            .ToPageListAsync(pageNumber,pageSize,totalNumber);

        return (totalNumber,dbList);
    }
    
    /// <inheritdoc />
    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return readOnlyClient.Queryable<TEntity>().CountAsync(whereExpression);
    }
    
    /// <inheritdoc />
    public Task<int> GetCountAsync()
    {
        return readOnlyClient.Queryable<TEntity>().CountAsync();
    }

    public Task<int> CountAsync(string sql, object whereObj)
    {
        return readOnlyClient.Ado.SqlQuerySingleAsync<int>(sql,whereObj);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return readOnlyClient.Queryable<TEntity>().AnyAsync(whereExpression);
    }
}