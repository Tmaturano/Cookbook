namespace Cookbook.Exceptions.ExceptionsBase;

public abstract class CookbookException : SystemException
{
    public IList<string> ErrorMessages { get; private set; } = new List<string>();
    protected CookbookException(IList<string> errorMessages) => ErrorMessages = errorMessages;

    protected CookbookException(string errorMessage) => ErrorMessages.Add(errorMessage);
}
