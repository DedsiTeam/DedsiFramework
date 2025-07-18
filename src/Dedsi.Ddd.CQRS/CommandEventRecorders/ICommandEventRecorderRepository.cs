using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.CQRS.CommandEventRecorders;
public interface ICommandEventRecorderRepository : ITransientDependency
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataId"></param>
    /// <param name="name"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    Task InsertAsync(Guid dataId, string name, string fullName, RecorderDataSource dataSource, CancellationToken cancellationToken);

    /// <summary>
    /// 保存至数据库
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task InsertAsync(CommandEventRecorder entity, CancellationToken cancellationToken);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="dataId"></param>
    /// <param name="name"></param>
    /// <param name="creatorName"></param>
    /// <returns></returns>
    Task<(long, CommandEventRecorder[])> GetPagedListAsync(
        int skipCount, 
        int maxResultCount, 
        string? name, 
        string? creatorName);

    /// <summary>
    /// 获得一组
    /// </summary>
    /// <param name="dataId"></param>
    /// <returns></returns>
    Task<CommandEventRecorder[]> GetByDataIdAsync(Guid dataId);
}
