namespace Dedsi.Ddd.Application.Contracts.Dtos;

[Serializable]
public abstract class AuditedEntityDto : CreationAuditedEntityDto
{
    public virtual DateTime? LastModificationTime { get; set; }

    public virtual Guid? LastModifierId { get; set; }
}

[Serializable]
public abstract class AuditedEntityDto<TKey> : CreationAuditedEntityDto<TKey>
{
    public virtual DateTime? LastModificationTime { get; set; }

    public virtual Guid? LastModifierId { get; set; }

}