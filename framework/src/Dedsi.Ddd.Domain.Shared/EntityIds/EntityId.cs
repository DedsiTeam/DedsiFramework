namespace Dedsi.Ddd.Domain.Shared.EntityIds;

public class EntityId : IEntityId;

public record StronglyTypedId<TSource> : IStronglyTypedId<TSource>
{
    public StronglyTypedId(TSource id)
    {
        Id = id;
    }

    public virtual TSource Id { get; }

    public override string ToString()
    {
        return $"Id = {Id}.";
    }
}

public record GuidStronglyTypedId(Guid Id) : StronglyTypedId<Guid>(Id), IGuidStronglyTypedId;

public record UlidStronglyTypedId(Ulid Id) : StronglyTypedId<Ulid>(Id), IUlidStronglyTypedId
{
    public UlidStronglyTypedId() : this(Ulid.NewUlid())
    {
        
    }

    /// <summary>
    /// id: Ulid字符串
    /// </summary>
    /// <param name="id"></param>
    public UlidStronglyTypedId(string id) : this(Ulid.Parse(id))
    {
        
    }
}