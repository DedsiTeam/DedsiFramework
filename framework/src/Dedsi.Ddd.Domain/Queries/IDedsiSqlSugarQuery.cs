using System.Linq.Expressions;

namespace Dedsi.Ddd.Domain.Queries;

public interface IDedsiSqlSugarQuery : IDedsiQuery
{
    Task<List<T>> GetListAsync<T>();

    Task<List<T>> GetListAsync<T>(string sql, object parameters = null);

    Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> expression);

    Task<T> FirstAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

    Task<T> FirstAsync<T>(string sql, object parameters = null);

    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression);

     Task<int> CountAsync<T>(Expression<Func<T, bool>> expression);

     Task<int> CountAsync<T>();
}