namespace Dedsi.AspNetCore.Middlewares;

/// <summary>仅用于 Dedsi 异常处理器，不改变 ABP 全局异常处理行为。</summary>
public class DedsiExceptionHandlingOptions
{
    /// <summary>允许展示的远程业务错误码及本地安全消息；默认不信任上游消息。</summary>
    public Dictionary<string, string> RemoteErrorMessages { get; } = new(StringComparer.Ordinal);
}
