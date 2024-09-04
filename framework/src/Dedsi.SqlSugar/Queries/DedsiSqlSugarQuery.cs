using System.Linq.Expressions;
using Dedsi.Ddd.Domain.Queries;
using SqlSugar;

namespace Dedsi.SqlSugar.Queries;

public class DedsiSqlSugarQuery(ISqlSugarClient sqlSugarClient) : IDedsiQuery
{
    protected virtual Task<List<T>> GetListAsync<T>()
    {
        return sqlSugarClient.Queryable<T>().ToListAsync();
    }
    
    protected virtual Task<List<T>> GetListAsync<T>(string sql, object parameters = null)
    {
        return sqlSugarClient.Ado.SqlQueryAsync<T>(sql, parameters);
    }

    protected virtual Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> expression)
    {
        return sqlSugarClient.Queryable<T>().Where(expression).ToListAsync();
    }
    
    protected virtual Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return sqlSugarClient.Queryable<T>().FirstAsync(expression, cancellationToken);
    }
    
    protected virtual Task<T> FirstAsync<T>(string sql, object parameters = null)
    {
        return sqlSugarClient.Ado.SqlQuerySingleAsync<T>(sql, parameters);
    }
    
    protected virtual Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression)
    {
        return sqlSugarClient.Queryable<T>().AnyAsync(expression);
    }
    
    protected virtual Task<int> CountAsync<T>(Expression<Func<T, bool>> expression)
    {
        return sqlSugarClient.Queryable<T>().CountAsync(expression);
    }
    
    protected virtual Task<int> CountAsync<T>()
    {
        return sqlSugarClient.Queryable<T>().CountAsync();
    }
    
}