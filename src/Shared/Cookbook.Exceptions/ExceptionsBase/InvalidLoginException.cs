namespace Cookbook.Exceptions.ExceptionsBase;

public class InvalidLoginException : CookbookException
{
    public InvalidLoginException(IList<string> errorMessages) : base(errorMessages) { }
}
