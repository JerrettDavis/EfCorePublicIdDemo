using EfCorePublicIdDemo.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EfCorePublicIdDemo.Application.Common.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Determines if the given public entity exists.
    /// </summary>
    /// <param name="queryable">The collection of public entities to search</param>
    /// <param name="publicId">The public ID of the entity to find</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>True if the entity exists, false if not.</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    public static Task<bool> PublicEntityExistsAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        string publicId, 
        CancellationToken cancellationToken = default) 
        where TEntity : class, IPublicEntity =>
        queryable.AnyAsync(e => e.PublicId == publicId, 
            cancellationToken);
    
    /// <summary>
    /// Returns the public entity matching the given ID or null if none is found
    /// </summary>
    /// <param name="queryable">The collection of public entities to search</param>
    /// <param name="publicId">The public ID of the entity to find</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The public entity matching the given ID or null if none is found</returns>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    public static Task<TEntity?> FindPublicEntityAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        string publicId,
        CancellationToken cancellationToken = default)
        where TEntity : class, IPublicEntity =>
        queryable.FirstOrDefaultAsync(e => e.PublicId == publicId,
            cancellationToken);

    /// <summary>
    /// Returns the public entity matching the given ID.
    /// </summary>
    /// <param name="queryable">The collection of public entities to search</param>
    /// <param name="publicId">The public ID of the entity to find</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The public entity matching the given ID</returns>
    /// <exception cref="InvalidOperationException">
    ///     <para>
    ///         No element found matching given <paramref name="publicId"/>
    ///     </para>
    ///     <para>
    ///         -or-
    ///     </para>
    ///     <para>
    ///         More than one element returned for <paramref name="publicId"/>
    ///     </para>
    /// </exception>
    /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
    public static Task<TEntity> SinglePublicEntityAsync<TEntity>(
        this IQueryable<TEntity> queryable,
        string publicId,
        CancellationToken cancellationToken = default)
        where TEntity : class, IPublicEntity =>
        queryable.SingleAsync(e => e.PublicId == publicId,
            cancellationToken);
}