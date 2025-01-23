using Volo.Abp.Auditing;

namespace Dedsi.Ddd.Domain.Auditing.Contracts;

/// <summary>
/// 创建人：姓名/Id/创建时间
/// </summary>
public interface IDedsiCreationAuditedObject : IDedsiMayHaveCreator, IHasCreationTime;