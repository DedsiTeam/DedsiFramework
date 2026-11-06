using System.Text.Encodings.Web;
using System.Text.Json;

namespace Dedsi.Core.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private static readonly JsonSerializerOptions NormalOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    /// <summary>
    ///  完整 JSON 序列化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJsonByNormal(this object obj)
    {
        return obj.ToJson(NormalOptions);
    }
    
    /// <summary>
    /// 序列化为对象
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T MapToObjectFromJson<T>(this string json)
    {
        return json.MapToObjectFromJson<T>(DefaultOptions);
    }


    /// <summary>
    /// 按调用者指定的配置反序列化，不修改配置。
    /// </summary>
    public static T MapToObjectFromJson<T>(this string json, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        return JsonSerializer.Deserialize<T>(json, options) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// 转为 json 字符串，默认驼峰命名
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        return obj.ToJson(DefaultOptions);
    }

    /// <summary>
    /// 转为 json 字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="jsonNamingPolicy"></param>
    /// <returns></returns>
    public static string ToJson(this object obj,JsonNamingPolicy jsonNamingPolicy)
    {
        return obj.ToJson(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = jsonNamingPolicy,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    /// <summary>
    /// 转为 json 字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="options">原样使用调用者配置，支持复用和只读配置。</param>
    /// <returns></returns>
    public static string ToJson(this object obj, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        return JsonSerializer.Serialize(obj, options);
    }

}