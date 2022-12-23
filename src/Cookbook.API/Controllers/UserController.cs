using Cookbook.API.Filters;
using Cookbook.Application.UseCases.User.Create;
using Cookbook.Application.UseCases.User.UpdatePassword;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> UpdatePassword([FromServices] IUpdatePasswordUseCase updateUserPasswordService,
    [FromBody] UpdatePasswordRequest request)
    {
        await updateUserPasswordService.ExecuteAsync(request);
        return NoContent();
    }
}
