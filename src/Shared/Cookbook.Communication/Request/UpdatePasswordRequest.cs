namespace Cookbook.Communication.Request;

public record UpdatePasswordRequest(string CurrentPassword, string NewPassword) { }
