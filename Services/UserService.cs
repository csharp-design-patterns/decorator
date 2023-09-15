using DecoratorPattern.Infrastructure.Data.Repositories.Logger;
using DecoratorPattern.Infrastructure.Data.Repositories.User;
using DecoratorPattern.Models;

namespace DecoratorPattern.Services;

public class UserService : IUserService
{
    private readonly ILoggerRepository<User, IUserRepository> _repository;

    public UserService(ILoggerRepository<User, IUserRepository> repository)
        => _repository = repository;

    public Task<Guid> RegisterAsync(User user)
        => _repository.ExecuteAsync(concrete => concrete.AddAsync(user));

    public async Task<User> RecoverAsync(Guid id)
    {
        var user = await _repository.ExecuteAsync(concrete => concrete.GetByIdAsync(id));
        return user ?? throw new Exception(nameof(User));
    }
}