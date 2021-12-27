using EfCorePublicIdDemo.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EfCorePublicIdDemo.Application.Common.Extensions;

public static class DbSetExtensions
{
    public static Task<bool> PublicEntityExistsAsync<TEntity>(
        this DbSet<TEntity> dbSet,
        string publicId, 
        CancellationToken cancellationToken = default) 
        where TEntity : class, IPublicEntity =>
        dbSet.AnyAsync(e => e.PublicId == publicId, 
            cancellationToken);

    public static Task<TEntity> PublicEntitySingleAsync<TEntity>(
        this DbSet<TEntity> dbSet,
        string publicId,
        CancellationToken cancellationToken = default)
        where TEntity : class, IPublicEntity =>
        dbSet.SingleAsync(e => e.PublicId == publicId,
            cancellationToken);
}