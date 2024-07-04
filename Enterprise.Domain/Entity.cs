namespace Enterprise.Domain;

public abstract class Entity<T> where T : Identity
{
    public T Id { get; init; }

    protected Entity(T id)
    {
        Id = id;
    }

    // TODO: ADD EQUALS OVERRIDE, IEQUTABLE, AND OPERATORS ==, !=

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
