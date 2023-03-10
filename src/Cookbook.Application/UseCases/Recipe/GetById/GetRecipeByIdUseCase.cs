using AutoMapper;
using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Exceptions;
using Cookbook.Exceptions.ExceptionsBase;

namespace Cookbook.Application.UseCases.Recipe.GetById;

public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IMapper _mapper;

    public GetRecipeByIdUseCase(IRecipeRepository recipeRepository, IAuthenticatedUser authenticatedUser, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _authenticatedUser = authenticatedUser;
        _mapper = mapper;
    }

    public async Task<RecipeRegisteredResponse> ExecuteAsync(Guid id)
    {
        var authenticatedUser = await _authenticatedUser.GetAsync();

        var recipe = await _recipeRepository.GetByIdWithNoTrackingAsync(id);

        Validate(authenticatedUser, recipe);

        return _mapper.Map<RecipeRegisteredResponse>(recipe);

    }

    public void Validate(Domain.Entities.User authenticatedUser, Domain.Entities.Recipe recipe)
    {
        if (recipe is null || recipe.OwnerId != authenticatedUser.Id)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.RecipeNotFound });
    }
}
