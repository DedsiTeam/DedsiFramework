using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Http.Client;
using Microsoft.Extensions.Options;

namespace Dedsi.AspNetCore.Middlewares;

/// <summary>
/// 全局异常中间件
/// </summary>
/// <param name="logger"></param>
public class DedsiGlobalExceptionHandler(ILogger<DedsiGlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var actualException = UnwrapSingleException(exception);

        // 已开始的响应和请求取消交给宿主处理，避免二次写入或将取消包装成服务器错误。
        if (httpContext.Response.HasStarted ||
            (actualException is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested))
        {
            return false;
        }

        // IExceptionHandler 是单例；从当前请求作用域解析 ABP 服务，避免捕获作用域依赖。
        var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder>();
        var converter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter>();
        var statusCode = (int)statusCodeFinder.GetStatusCode(httpContext, actualException);
        // ABP 会直接展示远程异常消息，并可能从 AggregateException 挑选第一个异常。
        // 这两种情况使用普通异常生成安全兜底消息，不改变原异常的状态码映射。
        var publicException = actualException is AbpRemoteCallException or AggregateException
            ? new Exception() : actualException;
        var error = converter.Convert(publicException, options =>
        {
            options.SendExceptionsDetailsToClients = false;
            options.SendStackTraceToClients = false;
        });
        if (actualException is AbpRemoteCallException remote && !string.IsNullOrWhiteSpace(remote.Error?.Code))
        {
            var settings = httpContext.RequestServices.GetRequiredService<IOptions<DedsiExceptionHandlingOptions>>().Value;
            if (settings.RemoteErrorMessages.TryGetValue(remote.Error.Code, out var safeMessage) && !string.IsNullOrWhiteSpace(safeMessage))
            {
                error.Message = safeMessage;
                error.Code = remote.Error.Code;
            }
        }
        var code = string.IsNullOrWhiteSpace(error.Code) ? $"global-error-{statusCode}" : error.Code;

        httpContext.Response.StatusCode = statusCode;
        logger.LogException(exception);

        // 保留现有响应契约，不输出异常详情、堆栈或 Data。
        await httpContext.Response.WriteAsJsonAsync(
            new DedsiErrorData(new DedsiErrorMessage(error.Message, code)), cancellationToken);

        return true;
    }

    private static Exception UnwrapSingleException(Exception exception)
    {
        if (exception is not AggregateException aggregate) return exception;
        var flattened = aggregate.Flatten();
        return flattened.InnerExceptions.Count == 1 ? flattened.InnerExceptions[0] : exception;
    }
}

/// <summary>
/// 
/// </summary>
/// <param name="Error"></param>
public record DedsiErrorData(DedsiErrorMessage Error);

/// <summary>
/// 错误数据结构
/// </summary>
/// <param name="Message"></param>
/// <param name="Code">如果有业务状态码就使用</param>
public record DedsiErrorMessage(string Message, string Code);
