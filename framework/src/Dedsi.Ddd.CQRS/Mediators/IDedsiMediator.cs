using Dedsi.Ddd.CQRS.Commands;

namespace Dedsi.Ddd.CQRS.Mediators;

public interface IDedsiMediator
{
    /// <summary>
    /// 发布命令：无返回值
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(IDedsiCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发布命令：有返回值
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    Task<TResponse> PublishAsync<TResponse>(IDedsiCommand<TResponse> request, CancellationToken cancellationToken = default);
}
