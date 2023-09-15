using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DecoratorPattern.Infrastructure.Data.Repositories.User;

public class UserRepository : Repository<Models.User>, IUserRepository
{
    public UserRepository(ApplicationContext context) 
        : base(context)
    {
    }

    public Task<List<Models.User>> GetAllAsync()
        => _context.Users.ToListAsync();
}
