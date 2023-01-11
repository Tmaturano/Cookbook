using Cookbook.API.Filters;
using Cookbook.Application.UseCases.Recipe.Create;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class RecipeController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(UserRegisteredResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserRegisteredResponse), StatusCodes.Status400BadRequest)]    
    public async Task<IActionResult> Post([FromServices] ICreateRecipeUseCase service,
        [FromBody] RegisterRecipeRequest request)
    {
        var response = await service.ExecuteAsync(request);
        return Created(string.Empty, response);
    }
}
