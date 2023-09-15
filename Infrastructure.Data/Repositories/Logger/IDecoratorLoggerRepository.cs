using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Models.Abstractions;
using System.Linq.Expressions;

namespace DecoratorPattern.Infrastructure.Data.Repositories.Logger;

public interface IDecoratorLoggerRepository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    Task<TOut> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, Task<TOut>>> execute, TIn @in);
    Task ExecuteAsync<TIn>(Expression<Func<TRepository, TIn, Task>> execute, TIn @in);
    ValueTask<TOut?> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, ValueTask<TOut?>>> execute,TIn @in);
}
