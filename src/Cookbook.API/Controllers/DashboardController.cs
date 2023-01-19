using Cookbook.API.Filters;
using Cookbook.Application.UseCases.Dashboard;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

public class DashboardController : BaseController
{
    [HttpPut()]
    [ProducesResponseType(typeof(DashboardResponse), StatusCodes.Status200OK)]    
    [ProducesResponseType(StatusCodes.Status204NoContent)]    
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetDashboard([FromServices] IDashboardUseCase useCase,
    [FromBody] DashboardRequest request)
    {
        var result = await useCase.ExecuteAsync(request);
        if (result.Recipes.Any()) 
            return Ok(result);

        return NoContent();
    }
}
