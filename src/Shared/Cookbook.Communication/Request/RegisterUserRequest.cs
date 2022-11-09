namespace Cookbook.Communication.Request;

public record RegisterUserRequest(string Name, string Email, string Password, string Phone);
