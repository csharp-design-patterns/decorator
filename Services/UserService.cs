using DecoratorPattern.Infrastructure.Data.Repositories.Logger;
using DecoratorPattern.Infrastructure.Data.Repositories.User;
using DecoratorPattern.Models;

namespace DecoratorPattern.Services;

public class UserService : IUserService
{
    private readonly IDecoratorLoggerRepository<User, IUserRepository> _repository;

    public UserService(IDecoratorLoggerRepository<User, IUserRepository> repository)
        => _repository = repository;

    public Task<Guid> RegisterAsync(User user)
        => _repository.ExecuteAsync((concrete, @in) => concrete.AddAsync(@in), user);

    public async Task<User> RecoverAsync(Guid id)
    {
        var user = await _repository.ExecuteAsync((concrete, @in) => concrete.GetByIdAsync(@in), id);
        return user ?? throw new Exception(nameof(User));
    }
}