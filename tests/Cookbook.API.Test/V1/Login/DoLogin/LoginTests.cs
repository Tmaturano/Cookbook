using Cookbook.Communication.Request;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Cookbook.API.Test.V1.Login.DoLogin;

public class LoginTests : ControllerBase
{
    private const string METHOD = "api/v1/login";
    private Domain.Entities.User _user;
    private string _password;

    public LoginTests(WebAppFactory<Program> factory) : base(factory)
    {
        _user = factory.User;
        _password = factory.Password;
    }

    [Fact]
    public async Task Validate_Success()
    {
        var request = new LoginRequest(_user.Email, _password);

        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_user.Name);
    }

}
