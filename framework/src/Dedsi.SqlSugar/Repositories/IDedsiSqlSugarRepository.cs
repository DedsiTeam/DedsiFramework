using SqlSugar;
using Volo.Abp.DependencyInjection;

namespace Dedsi.SqlSugar.Repositories;

/// <summary>
/// 异步方法优先
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IDedsiSqlSugarRepository<TEntity> 
    : ISimpleClient<TEntity>, ITransientDependency 
    where TEntity : class, new()
{
    
}