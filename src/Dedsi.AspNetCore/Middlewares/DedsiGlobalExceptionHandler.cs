using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Dedsi.AspNetCore.Middlewares;

/// <summary>
/// 全局异常中间件
/// </summary>
/// <param name="logger"></param>
public class DedsiGlobalExceptionHandler(ILogger<DedsiGlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        string code = "500";

        if (exception is UserFriendlyException userFriendlyException && !string.IsNullOrWhiteSpace(userFriendlyException.Code))
        {
            code = userFriendlyException.Code;
        }
        else if (exception is BusinessException businessException && !string.IsNullOrWhiteSpace(businessException.Code))
        {
            code = businessException.Code;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        logger.LogException(exception);

        await httpContext.Response.WriteAsJsonAsync(new DedsiErrorData(new DedsiErrorMessage(exception.Message, code)), cancellationToken);

        return true;
    }
}

public record DedsiErrorData(DedsiErrorMessage Error);

/// <summary>
/// 错误数据结构
/// </summary>
/// <param name="Message"></param>
/// <param name="Code">如果有业务状态码就使用</param>
public record DedsiErrorMessage(string Message,string Code)
{
    public string ServiceTime => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}