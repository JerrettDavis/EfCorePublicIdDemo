using EfCorePublicIdDemo.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EfCorePublicIdDemo.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static Task<bool> PublicEntityExistsAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        string publicId, 
        CancellationToken cancellationToken = default) 
        where TEntity : class, IPublicEntity =>
        queryable.AnyAsync(e => e.PublicId == publicId, 
            cancellationToken);

    public static Task<TEntity> PublicEntitySingleAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        string publicId,
        CancellationToken cancellationToken = default)
        where TEntity : class, IPublicEntity =>
        queryable.SingleAsync(e => e.PublicId == publicId,
            cancellationToken);
}