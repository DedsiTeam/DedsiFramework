using Microsoft.Extensions.Logging;

namespace Dedsi.Core.Extensions;

public static class LoggerExtensions
{
    // 分割线
    private const string PartingLine = "---------------------------------------------------------------------------------------------------------------------------------------------";
    
    public static void LogVitalInfo(this ILogger logger,string message)
    {
        logger.LogInformation(PartingLine);
        logger.LogInformation(message);
        logger.LogInformation(PartingLine);
    }
}