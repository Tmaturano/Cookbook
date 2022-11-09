namespace Cookbook.Communication.Response;

public class ErrorResponse
{
    public IEnumerable<string> Messages { get; set; }

	public ErrorResponse(string message)
	{
		Messages = new List<string>
		{
			message
		};		
	}

    public ErrorResponse(IEnumerable<string> messages) => Messages = messages;
}
