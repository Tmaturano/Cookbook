namespace Cookbook.Communication.Response;

public record DashboardResponse(IEnumerable<RecipeDashboardResponse> Recipes);
