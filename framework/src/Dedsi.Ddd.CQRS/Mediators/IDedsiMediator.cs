using Dedsi.Ddd.CQRS.Commands;
using Volo.Abp.DependencyInjection;

namespace Dedsi.Ddd.CQRS.Mediators;

public interface IDedsiMediator : ITransientDependency
{
    /// <summary>
    /// 发布命令：无返回值
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendAsync(IDedsiCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发布命令：有返回值
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<TResponse> SendAsync<TResponse>(IDedsiCommand<TResponse> request, CancellationToken cancellationToken = default);
}
