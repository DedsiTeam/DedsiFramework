namespace Dedsi.Ddd.Domain.Entities.Auditing;

[Serializable]
public abstract class CreationAuditedEntity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime CreationTime { get; protected set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public virtual Guid? CreatorId { get; protected set; }
}

[Serializable]
public abstract class CreationAuditedEntity<TKey> : Entity<TKey>
{
    public virtual DateTime CreationTime { get; protected set; }

    public virtual Guid? CreatorId { get; protected set; }

    protected CreationAuditedEntity()
    {

    }

    protected CreationAuditedEntity(TKey id)
        : base(id)
    {

    }
}