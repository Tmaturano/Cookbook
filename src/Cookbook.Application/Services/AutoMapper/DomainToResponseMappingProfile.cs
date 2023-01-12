using AutoMapper;
using Cookbook.Communication.Response;
using Cookbook.Domain.Entities;

namespace Cookbook.Application.Services.AutoMapper;

public class DomainToResponseMappingProfile : Profile
{
    public DomainToResponseMappingProfile()
    {
        CreateMap<Recipe, RecipeRegisteredResponse>()
            .ConstructUsing(request =>
                new RecipeRegisteredResponse(request.Id, request.Title, request.Category, request.PreparationMode)
            );

        CreateMap<Ingredient, IngredientRegisteredResponse>()
            .ConstructUsing(request => new IngredientRegisteredResponse(request.Id, request.Name, request.Quantity));

        CreateMap<Recipe, RecipeDashboardResponse>()
            .ConstructUsing(request => new RecipeDashboardResponse(request.Id, request.Title, request.Ingredients.Count));
    }
}
