using Yitter.IdGenerator;

namespace Dedsi.Ddd.Domain.Shared.EntityIds;

public class EntityId : IEntityId;

public record StronglyTypedId<TSource> : IStronglyTypedId<TSource>
{
    public virtual TSource Id { get; }
}

public record Int64StronglyTypedId(long Id) : StronglyTypedId<Int64>, IInt64StronglyTypedId
{
    public Int64StronglyTypedId() : this(YitIdHelper.NextId())
    {
    }
}

public record GuidStronglyTypedId(Guid Id) : StronglyTypedId<Guid>, IGuidStronglyTypedId;

public record UlidStronglyTypedId(Ulid Id) : StronglyTypedId<Ulid>, IUlidStronglyTypedId
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