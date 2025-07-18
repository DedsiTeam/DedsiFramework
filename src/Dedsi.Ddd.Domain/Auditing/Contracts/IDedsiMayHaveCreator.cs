using Volo.Abp.Auditing;

namespace Dedsi.Ddd.Domain.Auditing.Contracts;

/// <summary>
/// 创建人：姓名/Id
/// </summary>
public interface IDedsiMayHaveCreator : IHasCreationName, IMayHaveCreator;