using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Models.Abstractions;
using System.Linq.Expressions;

namespace DecoratorPattern.Infrastructure.Data.Repositories.Logger;

public interface ILoggerRepository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    Task<TOut> ExecuteAsync<TOut>(Expression<Func<TRepository, Task<TOut>>> execute);
    Task ExecuteAsync(Expression<Func<TRepository, Task>> execute);
    ValueTask<TOut?> ExecuteAsync<TOut>(Expression<Func<TRepository, ValueTask<TOut?>>> execute);
}
