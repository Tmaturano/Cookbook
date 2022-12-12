using Cookbook.Application.UseCases.Login.DoLogin;
using Cookbook.Communication.Request;
using Cookbook.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.API.Controllers;

public class LoginController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromServices] ILoginUseCase loginUseCase, 
        [FromBody] LoginRequest request)
    {
        var response = await loginUseCase.ExecuteAsync(request);
        return Ok(response);
    }
}
