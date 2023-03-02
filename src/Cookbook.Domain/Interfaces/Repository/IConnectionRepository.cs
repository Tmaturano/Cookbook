using Cookbook.Domain.Entities;

namespace Cookbook.Domain.Interfaces.Repository;

public interface IConnectionRepository : IRepositoryBase<Connection>
{
    Task<bool> ExistsBetween(Guid userId,  Guid connectedWithUserId);
}
