using Volo.Abp.Auditing;

namespace Dedsi.Ddd.Domain.Auditing.Contracts;

public interface IDedsiCreationAuditedObject : IHasCreationName, ICreationAuditedObject;