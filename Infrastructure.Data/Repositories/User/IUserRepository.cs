using DecoratorPattern.Infrastructure.Data.Abstractions;

namespace DecoratorPattern.Infrastructure.Data.Repositories.User;

public interface IUserRepository : IRepository<Models.User>
{
    Task<List<Models.User>> GetAllAsync();
}
