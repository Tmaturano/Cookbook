namespace Cookbook.Communication.Response;

public record RecipeDashboardResponse(Guid Id, string Title, int IngredientsQuantity, int PrepTime);
