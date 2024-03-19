namespace DDD.Service.Contracts;

public interface IBaseService<TEntity, in TId>
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    void UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    void DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}