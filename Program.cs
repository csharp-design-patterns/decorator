using DecoratorPattern.Infrastructure.Data.Abstractions;
using DecoratorPattern.Infrastructure.Data.Contexts;
using DecoratorPattern.Infrastructure.Data.Repositories.Logger;
using DecoratorPattern.Infrastructure.Data.Repositories.User;
using DecoratorPattern.Models;
using DecoratorPattern.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDbContext<ApplicationContext>(options 
    => options.UseInMemoryDatabase("decorator_pattern"));

services.AddTransient(typeof(ILoggerRepository<,>), typeof(LoggerRepository<,>));
services.AddTransient<IUserRepository, UserRepository>();
services.AddTransient<IUserService, UserService>();

//-------------------------------------------------------

var builder = services.BuildServiceProvider();

var service = builder.GetRequiredService<IUserService>();

var id = await service.RegisterAsync(new ()
{
    Name = "Jhon",
    Email = "jhon@hotmail.com"
});

var user = await service.RecoverAsync(id);

Console.WriteLine(user.Name);

Console.ReadKey();