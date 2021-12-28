using EfCorePublicIdDemo.Application.Common.Extensions;
using EfCorePublicIdDemo.Domain.Entities;
using EfCorePublicIdDemo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EfCorePublicIdDemo;

public class ConsoleWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ConsoleWorker> _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    public ConsoleWorker(
        IServiceProvider serviceProvider,
        ILogger<ConsoleWorker> logger,
        IHostApplicationLifetime appLifetime)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _appLifetime = appLifetime;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug(
            "Starting with arguments: {Arguments}", 
            string.Join(" ", Environment.GetCommandLineArgs()));

        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                var context = _serviceProvider.GetService<ApplicationDbContext>()!;
                _logger.LogInformation("Creating new entity");
                var entity = new MyEntity();

                await context.MyEntities.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Created new entity {Entity}", entity);
                
                _logger.LogInformation("Fetching all entities");
                var entities = await context.MyEntities.ToListAsync(cancellationToken);
                entities.ForEach(e => _logger.LogInformation("Returned {Entity}", e));
                
                _logger.LogInformation("Getting a random value");
                var random = Random.Shared.Next(0, entities.Count - 1);
                var randomEntity = entities[random];
                _logger.LogInformation("Getting entity by public ID");
                var entityByPublic = await context.MyEntities
                    .SinglePublicEntityAsync(randomEntity.PublicId, cancellationToken);
                _logger.LogInformation("Random Entity {Entity}", entityByPublic);
                
                _appLifetime.StopApplication();
            }, cancellationToken);
        });

        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Application stopping");
        
        return Task.CompletedTask;
    }
}