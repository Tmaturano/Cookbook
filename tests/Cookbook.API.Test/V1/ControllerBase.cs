using Cookbook.Communication.Request;
using Cookbook.Domain.Entities;
using Cookbook.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Cookbook.API.Test.V1;

public class ControllerBase : IClassFixture<WebAppFactory<Program>>
{
    private readonly HttpClient _client;

	public ControllerBase(WebAppFactory<Program> webAppFactory)
	{
		_client = webAppFactory.CreateClient();
        ErrorMessages.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        var jsonstring = JsonConvert.SerializeObject(body);
        return await _client.PostAsync(method, new StringContent(jsonstring, Encoding.UTF8, "application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string method, object body, string token = "")
    {
        AuthorizeRequest(token);
        
        var jsonstring = JsonConvert.SerializeObject(body);

        return await _client.PutAsync(method, new StringContent(jsonstring, Encoding.UTF8, "application/json"));
    }

    protected async Task<string> LoginAsync(string email, string password)
    {
        var request = new LoginRequest(email, password);

        var response = await PostRequest("api/v1/login", request);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        return responseData.RootElement.GetProperty("token").GetString() ?? string.Empty;
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
