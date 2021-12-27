// See https://aka.ms/new-console-template for more information

using EfCorePublicIdDemo;
using EfCorePublicIdDemo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder();
var host = builder
    .ConfigureServices((host, services) => Startup.ConfigureServices(host.Configuration, services))
    .UseConsoleLifetime()
    .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database");

        throw;
    }
}

await host.RunAsync();
