namespace Cookbook.Exceptions.ExceptionsBase;

public class ValidationErrorsException : CookbookException
{
    public IEnumerable<string> ErrorMessages { get; private set; }

	public ValidationErrorsException(IEnumerable<string> errorMessages)
	{
		ErrorMessages = errorMessages;
	}
}
