using Cookbook.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
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
}
