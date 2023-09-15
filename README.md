# Decorator Pattern

This is a structural design pattern, that allows an object to be dynamically enhanced with new behaviors without changing the original behavior.

## How it works

The decorator object wraps the original class, containing new behaviors, like the diagram below:

![Flow](./.assets/flow.png)

## About this project

In this case, we encapsulate the logger works, using the Clean Code strategies and the Decorator Pattern. 
When you want to logger a repository functionality, you going to call the Decorator Repository, informing the original Component

### Participants

* ConcreteComponent <sub>(UserRepository)</sub> - the original class that contains some behaviors
* Component <sub>(IUserRepository)</sub> - the interface that your original class implements
* ConcreteDecorator <sub>(LoggerRepository)</sub> - The class that extends of a Component's interface to use it and include some new behaviors
* Decorator <sub>ILoggerRepository</sub> - the interface that your ConcreteDecorator implements

### Let's the Code

***Attention Points:***
* Clean Code: it's important that your code is expressive, mainly the methods. Because, starting they that we are going to generate our message logs
* SOLID: our classes and methods should have unique responsibilities to make sense of our logs
* LOG: Logs are for the software engineer's observability and not for the final user or any product stakeholder. So we need to put messages that make sense for the engineering. This is an important thing, but many people forget it.

First, let's do our Component which going to contain our original behavior, like this:
``` csharp
public interface IRepository<TEntity> 
    where TEntity : Entity
{
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(Guid id);

}
```

Second we need a Decorator class that going to do some behaviors, like below:
``` csharp
public interface IDecoratorLoggerRepository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    Task<TOut> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, Task<TOut>>> execute, TIn @in);
    Task ExecuteAsync<TIn>(Expression<Func<TRepository, TIn, Task>> execute, TIn @in);
    ValueTask<TOut?> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, ValueTask<TOut?>>> execute,TIn @in);
}
```

Starting this, we allow us to execute any operations of the repository, which is our Component, that we pass in Generics Parameter, including other behaviors:

``` csharp
private readonly IDecoratorLoggerRepository<User, IUserRepository> _repository;

public async Task<User> RecoverAsync(Guid id)
{
    var user = await _repository.ExecuteAsync((concrete, @in) => concrete.GetByIdAsync(@in), id);
    return user ?? throw new Exception(nameof(User));
}
```

Or, we can use the only original class, which is our Component:

``` csharp
private readonly IUserRepository _userRepository;

public async Task<User> RecoverAsync(Guid id)
{
    var user = await _userRepository.GetByIdAsync(id);
    return user ?? throw new Exception(nameof(User));
}
```

## Pros and Cons

➕ Allow to add new behaviors without changing the original code </br>
➕ Easy to understand </br>
➕ You don't need to use inheritance every time </br>

➖ Complexity to debug </br>
➖ Complexity when the decorator interfaces is big

---

## References

* [C# Decorator Pattern: Why It's Still Useful in 2023](https://methodpoet.com/decorator-pattern/#:~:text=The%20Decorator%20is%20a%20structural,as%20the%20object%20it%20wraps)
* [Decorator Pattern ● Refactoring Guru](https://refactoring.guru/design-patterns/decorator)
* [C# Decorator](https://www.dofactory.com/net/decorator-design-pattern)
