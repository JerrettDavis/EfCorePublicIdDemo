using EfCorePublicIdDemo.Application.Common;
using EfCorePublicIdDemo.Infrastructure.Persistence;
using EfCorePublicIdDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfCorePublicIdDemo;

public static class Startup
{
    public static void ConfigureServices(
        IConfiguration configuration,
        IServiceCollection services)
    {
        services.Configure<IdGeneratorSettings>(configuration);
        services.AddHostedService<ConsoleWorker>();
        services.AddSingleton<IUniqueIdGenerator, UniqueIdGenerator>();
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
    }
}