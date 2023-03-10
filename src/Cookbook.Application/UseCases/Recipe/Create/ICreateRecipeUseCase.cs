using Cookbook.Communication.Request;
using Cookbook.Communication.Response;

namespace Cookbook.Application.UseCases.Recipe.Create;

public interface ICreateRecipeUseCase
{
    Task<RecipeRegisteredResponse> ExecuteAsync(RegisterRecipeRequest request);
}
