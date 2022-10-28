namespace Cookbook.Domain.Interfaces.Repository;

public interface IRepositoryBase<in TEntity> : IDisposable where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task<bool> ExistsAsync(Guid id);
}
