using Cookbook.Exceptions;
using FluentAssertions;
using Helper.Requests;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Cookbook.API.Test.V1.User.Create;

public class CreateUserTests : ControllerBase
{
	private const string METHOD = "api/v1/user";

	public CreateUserTests(WebAppFactory<Program> factory) : base(factory)
	{
	}

	[Fact]
	public async Task Validate_Success()
	{
		var request = RegisterUserRequestBuilder.Build();

		var response = await PostRequest(METHOD, request);

		response.StatusCode.Should().Be(HttpStatusCode.Created);

		await using var responseBody = await response.Content.ReadAsStreamAsync();
		var responseData = await JsonDocument.ParseAsync(responseBody);
		responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task Post_WhenNameIsEmptu_ShouldReturnError()
    {
        var request = RegisterUserRequestBuilder.Build();
		var requestWithError = request with
		{
			Name = string.Empty
		};

        var response = await PostRequest(METHOD, requestWithError);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
		var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
		errors.Should().ContainSingle().And.Contain(e => e.GetString().Equals(ErrorMessages.EmptyUserName));
    }
}
