namespace Cookbook.Communication.Request;

public record RegisterIngredientRequest(string Name, string Quantity)
{
    /*Workaround to work with Bogus and Record types*/
    public RegisterIngredientRequest() : this(Name: default, Quantity: default) { }
}
