using EfCorePublicIdDemo.Domain.Common;
using EfCorePublicIdDemo.Domain.Entities;
using EfCorePublicIdDemo.Infrastructure.Persistence.Extensions;
using EfCorePublicIdDemo.Infrastructure.Persistence.Generators;
using Microsoft.EntityFrameworkCore;

namespace EfCorePublicIdDemo.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<MyEntity> MyEntities => Set<MyEntity>();

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ConfigurePublicEntities();
        
        base.OnModelCreating(modelBuilder);
    }
}