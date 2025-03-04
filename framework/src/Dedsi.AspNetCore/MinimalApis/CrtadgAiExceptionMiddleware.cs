using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Volo.Abp;

namespace Dedsi.AspNetCore.MinimalApis;

/// <summary>
/// 处理错误中间件
/// </summary>
/// <param name="next"></param>
public class CrtadgAiExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
 
    /// <summary>
    /// 错误处理方法
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var requestInfo = new RequestInfo(httpContext.Request.Scheme, httpContext.Request.Host.ToString(),httpContext.Request.Path, httpContext.Request.Method);
        
        var errorResponse = new ErrorResponse(exception.Message, requestInfo);
        errorResponse.ChangeCode(httpContext.Response.StatusCode.ToString());
        
        if (exception is BusinessException businessException)
        {
            if (businessException.Code.IsNullOrWhiteSpace() == false)
            {
                errorResponse.ChangeCode(businessException.Code);
                errorResponse.ChangeDetails(businessException.Details);
            }
        }
 
        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        return httpContext.Response.WriteAsync(result);
    }
}

/// <summary>
/// 错误响应
/// </summary>
/// <param name="Message"></param>
/// <param name="RequestInfo">请求相关信息</param>
public record ErrorResponse(string Message, RequestInfo RequestInfo)
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public string? Code { get; private set; } = string.Empty;
    
    public void ChangeCode(string? newCode)
    {
        Code = newCode;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Details { get; set; }

    public void ChangeDetails(string? newDetails)
    {
        Details = newDetails;
    }
}

public record RequestInfo(string Scheme, string Host, string Path, string Method);