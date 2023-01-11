using AutoMapper;
using Cookbook.Application.Services.AuthenticatedUser;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Cookbook.Domain.Interfaces.Repository;
using Cookbook.Domain.Interfaces.UoW;
using Cookbook.Exceptions.ExceptionsBase;

namespace Cookbook.Application.UseCases.Recipe.Create;

public class CreateRecipeUseCase : ICreateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthenticatedUser _authenticatedUser;

    public CreateRecipeUseCase(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUser authenticatedUser)
    {
        _recipeRepository = recipeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<RecipeRegisteredResponse> ExecuteAsync(RegisterRecipeRequest request)
    {
        await ValidateAsync(request);

        var authenticatedUser = await _authenticatedUser.GetAsync();

        var recipe = _mapper.Map<Domain.Entities.Recipe>(request);
        recipe.AddOwner(authenticatedUser);

        await _recipeRepository.AddAsync(recipe);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<RecipeRegisteredResponse>(recipe);
    }

    private async Task ValidateAsync(RegisterRecipeRequest request)
    {
        var validator = new CreateRecipeValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
