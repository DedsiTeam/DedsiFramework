using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.CQRS.CommandEventRecorders;

/// <summary>
/// 时间命令和事件记录器接口。
/// </summary>
public interface ICqrsCeRecorder : ITransientDependency
{
    /// <summary>
    /// 记录器
    /// </summary>
    /// <param name="dataId"></param>
    /// <param name="name"></param>
    /// <param name="fullName"></param>
    /// <param name="dataSource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RecorderAsync(Guid dataId, string name, string fullName, RecorderDataSource dataSource, CancellationToken cancellationToken);
}
