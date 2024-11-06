namespace Dedsi.Ddd.Domain.Auditing.Contracts;

public interface IHasCreationName
{
    /// <summary>
    /// 创建人姓名
    /// </summary>
    string? CreatorName { get; }
}