using Cookbook.SharedKernel.Enum;

namespace Cookbook.Communication.Request;

public record DashboardRequest(string IngredientTitle, Category? category);
