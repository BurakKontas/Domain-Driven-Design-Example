using DDD.DataAccess.Contracts;
using DDD.Domain.Orders;
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDD.DataAccess.Repositories;

public abstract class BaseRepository<TEntity, TId>(ApplicationDbContext context) : IBaseRepository<TEntity, TId> where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var result = await context.Set<TEntity>().FindAsync([id], cancellationToken: cancellationToken);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

}
