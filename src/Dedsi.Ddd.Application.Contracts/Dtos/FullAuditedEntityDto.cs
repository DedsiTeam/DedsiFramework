namespace Dedsi.Ddd.Application.Contracts.Dtos;

[Serializable]
public abstract class FullAuditedEntityDto : AuditedEntityDto
{
    public virtual bool IsDeleted { get; set; }

    public virtual Guid? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }
}

[Serializable]
public abstract class FullAuditedEntityDto<TKey> : AuditedEntityDto<TKey>
{
    public virtual bool IsDeleted { get; set; }

    public virtual Guid? DeleterId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }

}