namespace Dedsi.Ddd.Domain.Entities.Auditing;

[Serializable]
public abstract class AuditedEntity : CreationAuditedEntity
{
    public virtual DateTime? LastModificationTime { get; set; }

    public virtual Guid? LastModifierId { get; set; }
}

[Serializable]
public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>
{
    public virtual DateTime? LastModificationTime { get; set; }

    public virtual Guid? LastModifierId { get; set; }

    protected AuditedEntity()
    {

    }

    protected AuditedEntity(TKey id) : base(id)
    {

    }
}