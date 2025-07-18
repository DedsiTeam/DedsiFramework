using Dedsi.Ddd.Domain.Queries;
using SqlSugar;

namespace Dedsi.SqlSugar.Queries;

/// <summary>
/// SqlSugar 实现的查询接口
/// </summary>
/// <param name="sqlSugarClient"></param>
public class DedsiSqlSugarQuery(ISqlSugarClient sqlSugarClient) : IDedsiQuery
{
    protected virtual ValueTask<ISugarQueryable<TEntity>> GetQueryableAsync<TEntity>() => ValueTask.FromResult(sqlSugarClient.Queryable<TEntity>());
}