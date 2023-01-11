using Cookbook.SharedKernel.Enum;

namespace Cookbook.Communication.Response;

public record RecipeRegisteredResponse(Guid Id, string Title, Category Category, string PreparationMode)
{
    public List<IngredientRegisteredResponse> Ingredients { get; init; } = default;
}


