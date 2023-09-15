using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Infrastructure.Data.Contexts;
using DecoratorPattern.Infrastructure.Data.Repositories.Logger;
using DecoratorPattern.Infrastructure.Data.Repositories.User;
using DecoratorPattern.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDbContext<ApplicationContext>(options 
    => options.UseInMemoryDatabase("decorator_pattern"));

services.AddTransient(typeof(ILoggerRepository<,>), typeof(LoggerRepository<,>));
services.AddTransient<IUserRepository, UserRepository>();

//-------------------------------------------------------

var builder = services.BuildServiceProvider();

var loggerRepository = builder.GetRequiredService<ILoggerRepository<User, IUserRepository>>();
var userRepository = builder.GetRequiredService<IUserRepository>();

// With decorator
var id = await loggerRepository.ExecuteAsync(concrete => concrete.AddAsync(new User()
{
    Name = "Alex Alves",
    Email = "alexalves2501@hotmail.com"
}));

// Without decorator
var id2 = await userRepository.AddAsync(new User()
{
    Name = "Alex Alves",
    Email = "alexalves2501@hotmail.com"
});

var user = await loggerRepository.ExecuteAsync(concrete => concrete.GetByIdAsync(id2));

var users = await loggerRepository.ExecuteAsync(concrete => concrete.GetAllAsync());

Console.ReadKey();