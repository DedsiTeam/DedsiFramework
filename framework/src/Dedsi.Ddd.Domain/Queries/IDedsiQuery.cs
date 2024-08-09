using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.Domain.Queries;

/// <summary>
/// 查询
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IDedsiQuery : ITransientDependency
{

}