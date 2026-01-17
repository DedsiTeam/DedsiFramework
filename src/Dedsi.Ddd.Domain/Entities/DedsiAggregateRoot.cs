using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp.Domain.Entities;

namespace Dedsi.Ddd.Domain.Entities;

public class DedsiAggregateRoot<TKey>(TKey id) : AggregateRoot<TKey>(id), IDedsiCreationAuditedObject
{

    public string CreatorName { get; private set; }

    public Guid CreatorId { get; private set; }

    public DateTime CreationTime { get; private set; }
}
