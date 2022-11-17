using Cookbook.Application.UseCases.User.Create;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost()]
    [ProducesResponseType(typeof(UserRegisteredResponse), 201)]
    [ProducesResponseType(typeof(UserRegisteredResponse), 400)]
    public async Task<IActionResult> Post([FromServices] ICreateUserUseCase createUserService,
        [FromBody]RegisterUserRequest request)
    {
        var response = await createUserService.ExecuteAsync(request);
        return Ok(response);     
    }        
}
