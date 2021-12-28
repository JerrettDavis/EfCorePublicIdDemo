using EfCorePublicIdDemo.Domain.Common;
using EfCorePublicIdDemo.Infrastructure.Persistence.Generators;
using Microsoft.EntityFrameworkCore;

namespace EfCorePublicIdDemo.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigurePublicEntities(this ModelBuilder modelBuilder)
    {
        var publicEntities = modelBuilder.Model.GetEntityTypes()
            .Where(i => i.ClrType.IsAssignableTo(typeof(IPublicEntity)));
        foreach (var item in publicEntities)
        {
            modelBuilder.Entity(item.ClrType)
                .Property(nameof(IPublicEntity.PublicId))
                .HasValueGenerator<PublicIdValueGenerator>();
        }
    }
}