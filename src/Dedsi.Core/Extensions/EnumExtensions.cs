using System.ComponentModel;

namespace Dedsi.Core.Extensions;

public static class EnumExtensions
{
    
    /// <summary>
    /// 读取-Description
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string GetDescriptionValue(this Enum val)
    {
        var field = val.GetType().GetField(val.ToString());
        var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return customAttribute == null ? "" : ((DescriptionAttribute)customAttribute).Description;
    }

}