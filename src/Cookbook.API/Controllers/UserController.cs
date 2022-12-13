using Cookbook.Application.UseCases.User.Create;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(UserRegisteredResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserRegisteredResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromServices] ICreateUserUseCase createUserService,
        [FromBody]RegisterUserRequest request)
    {
        var response = await createUserService.ExecuteAsync(request);
        return Created(string.Empty,response);
    }        
}
