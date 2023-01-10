using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;

namespace Cookbook.Infrastructure.Data.Repository;

public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
{
    public RecipeRepository(CookbookContext context) : base(context)
    {
    }
}
