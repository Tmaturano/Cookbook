using Cookbook.Domain.Entities;
using Cookbook.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Infrastructure.Data.Repository;

public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
{
    public RecipeRepository(CookbookContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Recipe>> GetAllByUserIdAsync(Guid userId)
        => await DbSet.AsNoTracking()
            .Include(x => x.Ingredients)
            .Where(x => x.OwnerId == userId).ToListAsync();

    public async Task<Recipe> GetByIdWithNoTrackingAsync(Guid recipeId)
        => await DbSet.AsNoTracking()
            .Include(x => x.Ingredients)
            .FirstOrDefaultAsync(x => x.Id == recipeId);
}
