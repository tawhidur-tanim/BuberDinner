namespace BuberDinner.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : notnull

{
    protected AggregateRoot(TId id) : base(id)
    {
        Id = id;
    }


}