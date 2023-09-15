using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Models.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using System.Text.Json;

namespace DecoratorPattern.Infrastructure.Data.Repositories.Logger;

public class DecoratorLoggerRepository<TEntity, TRepository> : IDecoratorLoggerRepository<TEntity, TRepository>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    private readonly TRepository _repository;

    public DecoratorLoggerRepository(TRepository repository)
        => _repository = repository;

    public async Task<TOut> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, Task<TOut>>> execute, TIn @in)
    {
        try
        {
            LogInformtion(execute, "Start", @in);
            var result = await execute.Compile()(_repository, @in);
            LogInformtion(execute, "Success", @in, result);
            return result;
        }
        catch (Exception ex)
        {
            LogError(execute, "Error", @in, ex);
            throw;
        }
    }

    public async Task ExecuteAsync<TIn>(Expression<Func<TRepository, TIn, Task>> execute, TIn @in)
    {
        try
        {
            LogInformtion(execute, "Start", @in);
            await execute.Compile()(_repository, @in);
            LogInformtion(execute, "Success", @in);
        }
        catch (Exception ex)
        {
            LogError(execute, "Error", @in, ex);
            throw;
        }
    }

    public async ValueTask<TOut?> ExecuteAsync<TIn, TOut>(Expression<Func<TRepository, TIn, ValueTask<TOut?>>> execute, TIn @in)
    {
        try
        {
            LogInformtion(execute, "Start", @in);
            var result = await execute.Compile()(_repository, @in);
            LogInformtion(execute, "Success", @in);
            return result;
        }
        catch (Exception ex)
        {
            LogError(execute, "Error", @in, ex);
            throw;
        }
    }

    private static void LogInformtion<TExpression>(Expression<TExpression> expression, string status, object input, object? output = null)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(@$"
            [Operation]: {((MethodCallExpression)expression.Body).Method.Name} | 
            [Entity]: {typeof(TEntity).Name} | 
            [Status]: {status} | 
            [Input]: {JsonSerializer.Serialize(input)} |
            {(output is not null ? $"[Output]: {JsonSerializer.Serialize(output)}" : string.Empty)}");
        Console.ResetColor();
    }

    private static void LogError<TExpression>(Expression<TExpression> expression, string status, object input, Exception exception)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(@$"
            [Operation]: {((MethodCallExpression)expression.Body).Method.Name} | 
            [Entity]: {typeof(TEntity).Name} | 
            [Status]: {status} | 
            [Input]: {JsonSerializer.Serialize(input)}
            [Errro]: {exception.Message}");
        Console.ResetColor();
    }
}