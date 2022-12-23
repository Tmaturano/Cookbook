namespace Cookbook.Communication.Request;

public record UpdatePasswordRequest(string CurrentPassword, string NewPassword)
{
    /*Workaround to work with Bogus and Record types*/
    public UpdatePasswordRequest() : this(CurrentPassword: default, NewPassword: default) { }
}

