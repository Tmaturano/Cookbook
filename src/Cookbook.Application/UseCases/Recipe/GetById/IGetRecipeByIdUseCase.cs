using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.Recipe.GetById;

public interface IGetRecipeByIdUseCase
{
    Task<RecipeRegisteredResponse> ExecuteAsync(Guid id);
}
