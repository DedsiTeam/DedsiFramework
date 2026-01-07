using System.Text.Encodings.Web;
using System.Text.Json;

namespace Dedsi.Core.Extensions;

public static class JsonExtensions
{
    /// <summary>
    ///  完整 JSON 序列化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJsonByNormal(this object obj)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = null
        };
        return obj.ToJson(options);
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
        return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException();
    }


    /// <summary>
    /// 转为 json 字符串，默认驼峰命名
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        return obj.ToJson(JsonNamingPolicy.CamelCase);
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
    /// <param name="option"></param>
    /// <returns></returns>
    public static string ToJson(this object obj, JsonSerializerOptions options)
    {
        options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        return JsonSerializer.Serialize(obj, options);
    }

}