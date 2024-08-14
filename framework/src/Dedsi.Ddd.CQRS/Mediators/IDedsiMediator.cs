using Dedsi.Ddd.CQRS.Commands;

namespace Dedsi.Ddd.CQRS.Mediators;

public interface IDedsiMediator
{
    /// <summary>
    /// 无返回值
    /// </summary>
    /// <param name="command"></param>
    /// <param name="onUnitOfWorkComplete"></param>
    /// <returns></returns>
    Task PublishAsync(IDedsiCommand command, bool onUnitOfWorkComplete = true);

    /// <summary>
    /// 有返回值
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse> PublishAsync<TResponse>(IDedsiCommand<TResponse> request, CancellationToken cancellationToken = default);
}
