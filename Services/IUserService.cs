using DecoratorPattern.Models;

namespace DecoratorPattern.Services;

public interface IUserService
{
    Task<Guid> RegisterAsync(User user);
    Task<User> RecoverAsync(Guid id);
}
