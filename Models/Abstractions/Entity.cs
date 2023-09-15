namespace DecoratorPattern.Models.Abstractions;

public abstract record Entity
{
    public Guid Id { get; private set;  } = Guid.NewGuid();
}
