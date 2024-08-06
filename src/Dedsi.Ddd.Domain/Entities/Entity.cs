namespace Dedsi.Ddd.Domain.Entities;

public abstract class Entity : IEntity
{
    
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    protected Entity()
    {

    }

    protected Entity(TKey id)
    {
        Id = id;
    }
    
    /// <summary>
    /// 主键Id
    /// </summary>
    public TKey Id { get; protected set; }
    
    
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}