namespace Dedsi.Ddd.Application.Contracts.Dtos;

public abstract class EntityDto : IEntityDto
{
    public override string ToString()
    {
        return $"[DTO: {GetType().Name}]";
    }
}

public abstract class EntityDto<TKey> : EntityDto, IEntityDto<TKey>
{
    public TKey Id { get; set; }

    public override string ToString()
    {
        return $"[DTO: {GetType().Name}] Id = {Id}";
    }
}