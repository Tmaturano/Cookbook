using Cookbook.Application.UseCases.User.Create;
using Cookbook.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpPost()]
    //[ProducesResponseType(typeof(string), 201)]    
    public async Task<IActionResult> Post([FromServices] ICreateUserUseCase createUserService,
        [FromBody]RegisterUserRequest request)
    {
        await createUserService.ExecuteAsync(request);

        return Ok();     
    }        
}
