using AutoMapper;
using Cookbook.Communication.Request;
using Cookbook.Domain.Entities;

namespace Cookbook.Application.Services.AutoMapper;

public class ResponseToDomainMappingProfile : Profile
{
	public ResponseToDomainMappingProfile()
	{
		CreateMap<RegisterUserRequest, User>()
			.ConstructUsing(request => new User(request.Name, request.Email, request.Phone))
			.ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

		CreateMap<RegisterRecipeRequest, Recipe>()
			.ConstructUsing(request => new Recipe(request.Title, request.Category, request.PreparationMode));

		CreateMap<RegisterIngredientRequest, Ingredient>()
			.ConstructUsing(request => new Ingredient(request.Name, request.Quantity));
	}
}
