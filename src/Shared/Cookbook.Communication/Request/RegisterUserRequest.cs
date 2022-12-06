namespace Cookbook.Communication.Request;

public record RegisterUserRequest(string Name, string Email, string Password, string Phone)
{
    /*Workaround to work with Bogus and Record types*/
    public RegisterUserRequest() : this(Name: default, Email: default, Password: default, Phone: default) { }
}
