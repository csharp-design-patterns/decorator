using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Models.Abstractions;
using System.Linq.Expressions;

namespace DecoratorPattern.Infrastructure.Data.Repositories.Logger;

public class LoggerRepository<TEntity, TRepository> : ILoggerRepository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    private readonly TRepository _repository;

    public LoggerRepository(TRepository repository)
        => _repository = repository;

    public async Task<TOut> ExecuteAsync<TOut>(Expression<Func<TRepository, Task<TOut>>> execute)
    {
        try
        {
            LogInformtion(execute, "Start");
            var compiled = execute.Compile();
            var result = await compiled(_repository);
            LogInformtion(execute, "Success");
            return result;
        }
        catch (Exception ex)
        {
            LogInformtion(execute, "Error");
            throw;
        }
    }

    public async Task ExecuteAsync(Expression<Func<TRepository, Task>> execute)
    {
        try
        {
            LogInformtion(execute, "Start");
            var compiled = execute.Compile();
            await compiled(_repository);
            LogInformtion(execute, "Success");
        }
        catch (Exception ex)
        {
            LogInformtion(execute, "Error");
            throw;
        }
    }

    public async ValueTask<TOut?> ExecuteAsync<TOut>(Expression<Func<TRepository, ValueTask<TOut?>>> execute)
    {
        try
        {
            LogInformtion(execute, "Start");
            var compiled = execute.Compile();
            var result = await compiled(_repository);
            LogInformtion(execute, "Success");
            return result;
        }
        catch (Exception ex)
        {
            LogInformtion(execute, "Error");
            throw;
        }
    }

    private static void LogInformtion<TExpression>(Expression<TExpression> expression, string status)
        => Console.WriteLine($"[Operation]: {((MethodCallExpression)expression.Body).Method.Name} | [Entity]: {typeof(TEntity).Name} | [Status]: {status}");
}