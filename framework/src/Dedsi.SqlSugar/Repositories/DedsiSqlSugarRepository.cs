using SqlSugar;
using Volo.Abp.Domain.Entities;

namespace Dedsi.SqlSugar.Repositories;

/// <summary>
/// 异步方法优先
/// </summary>
/// <param name="sqlSugarClient"></param>
/// <typeparam name="TEntity"></typeparam>
public class DedsiSqlSugarRepository<TEntity>(ISqlSugarClient sqlSugarClient) : SimpleClient<TEntity>(sqlSugarClient), IDedsiSqlSugarRepository<TEntity> where TEntity : class, IEntity, new()
{

}

