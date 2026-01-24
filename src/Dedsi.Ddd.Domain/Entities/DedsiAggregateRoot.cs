using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp.Domain.Entities;

namespace Dedsi.Ddd.Domain.Entities;

public class DedsiAggregateRoot<TKey> : AggregateRoot<TKey>, IDedsiCreationAuditedObject
{

    public string CreatorName { get; protected set; }

    public Guid CreatorId { get; protected set; }

    public DateTime CreationTime { get; protected set; }

    public DedsiAggregateRoot()
    {

    }

    public DedsiAggregateRoot(TKey id) : base(id) { }
}
