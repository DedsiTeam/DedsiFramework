using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Dedsi.Core.Extensions;

/// <summary>
/// string 扩展类
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 字符串转byte[]
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static byte[] ToBytes(this string text)
    {
        return Encoding.UTF8.GetBytes(text);
    }

    /// <summary>
    /// byte[] 转字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ByteToString(this byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
    
    /// <summary>
    /// 小驼峰命名
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return JsonNamingPolicy.CamelCase.ConvertName(text);
    }

    /// <summary>
    /// 传入一段 html 富文本，过滤所有标签，只保留文字。
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string StripHtml(this string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        // 去除 script 与 style 内容
        string noScriptStyle = Regex.Replace(html, @"<\s*(script|style)[^>]*>.*?<\s*/\s*\1\s*>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // 去除所有标签
        string noTags = Regex.Replace(noScriptStyle, @"<[^>]+>", string.Empty);

        // 将多个空白压缩为单个空格
        string normalized = Regex.Replace(noTags, @"\s+", " ").Trim();

        // 解码 HTML 实体（如 \&amp; \&lt; 等）
        string decoded = WebUtility.HtmlDecode(normalized);

        return decoded;
    }
}