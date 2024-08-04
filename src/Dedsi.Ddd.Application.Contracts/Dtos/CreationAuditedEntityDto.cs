namespace Dedsi.Ddd.Application.Contracts.Dtos;

[Serializable]
public abstract class CreationAuditedEntityDto
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
public abstract class CreationAuditedEntityDto<TKey> : EntityDto<TKey>
{
    public virtual DateTime CreationTime { get; protected set; }

    public virtual Guid? CreatorId { get; protected set; }

}