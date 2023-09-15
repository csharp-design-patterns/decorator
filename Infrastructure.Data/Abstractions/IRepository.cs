using DecoratorPattern.Models.Abstractions;

namespace DecoratorPattern.Infrastructure.Data.Abstractions;

public interface IRepository<TEntity> 
    where TEntity : Entity
{
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(Guid id);

}
