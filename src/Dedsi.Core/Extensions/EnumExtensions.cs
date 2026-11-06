using System.ComponentModel;

namespace Dedsi.Core.Extensions;

public static class EnumExtensions
{
    
    /// <summary>
    /// 读取 Description；未定义值、无独立字段的 Flags 组合或无描述时返回空字符串。
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetDescriptionValue(this Enum val)
    {
        ArgumentNullException.ThrowIfNull(val);
        var field = val.GetType().GetField(val.ToString());
        if (field == null) return string.Empty;
        var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return customAttribute == null ? "" : ((DescriptionAttribute)customAttribute).Description;
    }

}