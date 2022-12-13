namespace Cookbook.Exceptions.ExceptionsBase;

public class ValidationErrorsException : CookbookException
{
	public ValidationErrorsException(IList<string> errorMessages) : base(errorMessages)	{ }
}
