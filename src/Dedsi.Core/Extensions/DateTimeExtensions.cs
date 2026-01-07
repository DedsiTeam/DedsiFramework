namespace Dedsi.Core.Extensions;

public static class DateTimeExtensions
{
    public static string GetNowString() => GetNowString("yyyy-MM-dd HH:mm:ss");
    
    public static string GetNowString(string format) => DateTime.Now.ToString(format);
}