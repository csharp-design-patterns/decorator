using DecoratorPattern.Models.Abstractions;

namespace DecoratorPattern.Models;

public record User : Entity
{
    public string Name { get; init; }

    public string Email { get; init; }
}
