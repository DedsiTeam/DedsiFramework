namespace Dedsi.Ddd.Domain.Auditing.Contracts;

/// <summary>
/// 创建人：姓名
/// </summary>
public interface IHasCreationName
{
    /// <summary>
    /// 创建人姓名
    /// </summary>
    string? CreatorName { get; }
}