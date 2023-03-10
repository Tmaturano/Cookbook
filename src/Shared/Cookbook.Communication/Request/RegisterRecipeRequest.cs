using Cookbook.SharedKernel.Enum;

namespace Cookbook.Communication.Request;

public record RegisterRecipeRequest(string Title, Category Category, string PreparationMode, List<RegisterIngredientRequest> Ingredients,
    int PrepTime)
{
    /*Workaround to work with Bogus and Record types*/
    public RegisterRecipeRequest() : this(Title: default, Category: default, PreparationMode: default, Ingredients: new(), PrepTime: default) { }
}
