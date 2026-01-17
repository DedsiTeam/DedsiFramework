namespace Dedsi.Ddd.Domain.Auditing.Contracts;

/// <summary>
/// 创建人：Id
/// </summary>
public interface IDedsiHasCreationId
{
    /// <summary>
    /// 创建人Id
    /// </summary>
    Guid CreatorId { get; }
}