using DecoratorPattern.Infrastructure.Data.Contexts;
using DecoratorPattern.Models.Abstractions;

namespace DecoratorPattern.Infrastructure.Data.Abstractions;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly ApplicationContext _context;

    public Repository(ApplicationContext context)
        => _context = context;

    public async Task<Guid> AddAsync(TEntity entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        return _context.SaveChangesAsync();
    }

    public ValueTask<TEntity?> GetByIdAsync(Guid id)
        => _context.FindAsync<TEntity>(id);

    public Task UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        return _context.SaveChangesAsync();
    }
}
