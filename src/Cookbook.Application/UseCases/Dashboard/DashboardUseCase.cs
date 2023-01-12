using AutoMapper;
using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;

namespace Cookbook.Application.UseCases.Dashboard;

public class DashboardUseCase : IDashboardUseCase
{
    private readonly IRecipeRepository _repository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IMapper _mapper;

    public DashboardUseCase(IRecipeRepository repository, IAuthenticatedUser authenticatedUser, IMapper mapper)
    {
        _repository = repository;
        _authenticatedUser = authenticatedUser;
        _mapper = mapper;
    }

    public async Task<DashboardResponse> ExecuteAsync(DashboardRequest request)
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();

        var recipes = await _repository.GetAllByUserIdAsync(authenticatedUser.Id);

        recipes = Filter(request, recipes);

        return new DashboardResponse(_mapper.Map<IEnumerable<RecipeDashboardResponse>>(recipes));
    }

    private IEnumerable<Domain.Entities.Recipe> Filter(DashboardRequest request, IEnumerable<Domain.Entities.Recipe> recipes)
    {
        var filteredRecipes = recipes;

        if (request.category.HasValue)
            filteredRecipes = recipes.Where(x => x.Category == request.category.Value);

        if (!string.IsNullOrWhiteSpace(request.IngredientTitle))
            filteredRecipes = filteredRecipes.Where(x => x.Title.Contains(request.IngredientTitle) 
            || x.Ingredients.Any(ingredient => ingredient.Name.Contains(request.IngredientTitle)));

        return filteredRecipes;
    }
}
