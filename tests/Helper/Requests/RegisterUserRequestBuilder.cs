using Bogus;
using Cookbook.Communication.Request;

namespace Helper.Requests;

public static class RegisterUserRequestBuilder
{
    public static RegisterUserRequest Build(int passwordLength = 10)
    {
        return new Faker<RegisterUserRequest>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"))
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordLength)).Generate();
    }
}
