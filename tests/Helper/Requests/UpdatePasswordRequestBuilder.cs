using Bogus;
using Cookbook.Communication.Request;

namespace Helper.Requests;

public static class UpdatePasswordRequestBuilder
{
    public static UpdatePasswordRequest Build(int passwordLength = 10)
    {
        return new Faker<UpdatePasswordRequest>()
            .RuleFor(c => c.CurrentPassword, f => f.Internet.Password(10))
            .RuleFor(c => c.NewPassword, f => f.Internet.Password(passwordLength)).Generate();
    }
}
