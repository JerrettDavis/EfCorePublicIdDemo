using EfCorePublicIdDemo.Domain.Common;
using EfCorePublicIdDemo.Infrastructure.Persistence.Generators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCorePublicIdDemo.Infrastructure.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void AddPublicIdGenerator<TEntity>(
        this EntityTypeBuilder<TEntity> builder) 
        where TEntity : class, IPublicEntity
    {
        builder.Property(e => e.PublicId)
            .HasValueGenerator<PublicIdValueGenerator>();
    }
}