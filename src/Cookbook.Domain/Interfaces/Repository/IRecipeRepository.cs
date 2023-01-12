using Cookbook.Domain.Entities;

namespace Cookbook.Domain.Interfaces.Repository;

public interface IRecipeRepository : IRepositoryBase<Recipe>
{
    Task<IEnumerable<Recipe>> GetAllByUserIdAsync(Guid userId);
}
