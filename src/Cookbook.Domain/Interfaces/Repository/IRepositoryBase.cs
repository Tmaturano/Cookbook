namespace Cookbook.Domain.Interfaces.Repository;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
{
    Task AddAsync(TEntity obj);
    void Update(TEntity obj);
    Task<bool> ExistsAsync(Guid id);
    Task<TEntity> GetByIdAsync(Guid id);
}
