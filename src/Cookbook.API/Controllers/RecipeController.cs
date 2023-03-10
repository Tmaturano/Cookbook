using Cookbook.API.Filters;
using Cookbook.Application.UseCases.Recipe.Create;
using Cookbook.Application.UseCases.Recipe.GetById;
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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RecipeRegisteredResponse), StatusCodes.Status200OK)]    
    public async Task<IActionResult> Get([FromServices] IGetRecipeByIdUseCase useCase, 
        [FromRoute] Guid id)
    {
        var response = await useCase.ExecuteAsync(id);
        return Ok(response);
    }
}
