using DecoratorPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorPattern.Infrastructure.Data.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions)
        :base(dbContextOptions) { }

    public DbSet<User> Users { get; set; }
}
