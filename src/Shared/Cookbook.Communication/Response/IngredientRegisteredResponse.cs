namespace Cookbook.Communication.Response;

public record IngredientRegisteredResponse(Guid Id, string Name, string Quantity);