using Dedsi.Ddd.Domain.Auditing.Contracts;
using Volo.Abp.Domain.Entities;

namespace Dedsi.Ddd.Domain.Entities;

public class DedsiAggregateRoot<TKey> : AggregateRoot<TKey>, IDedsiCreationAuditedObject
{

    public string CreatorName { get; private set; }

    public Guid CreatorId { get; private set; }

    public DateTime CreationTime { get; private set; }

    public DedsiAggregateRoot()
    {

    }

    public DedsiAggregateRoot(TKey id) : base(id) { }
}
