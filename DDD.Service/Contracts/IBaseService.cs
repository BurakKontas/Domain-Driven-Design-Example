namespace DDD.Service.Contracts;

public interface IBaseService<TEntity, in TId>
{
    Task<TEntity?> CreateAsync(TEntity? entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity? entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
}