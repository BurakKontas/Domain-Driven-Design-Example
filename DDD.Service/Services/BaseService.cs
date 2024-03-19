using DDD.DataAccess.Contracts;
using DDD.Service.Contracts;

namespace DDD.Service.Services;

public class BaseService<TEntity, TId>(IBaseRepository<TEntity, TId> repository) : IBaseService<TEntity, TId> where TEntity : class
{
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await repository.GetAllAsync(cancellationToken);
    }

    public void UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        repository.UpdateAsync(entity, cancellationToken);
    }

    public async void DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
            repository.DeleteAsync(entity, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await repository.SaveChangesAsync(cancellationToken);
    }
}