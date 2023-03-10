namespace Cookbook.Domain.Entities;

public class Ingredient : EntityBase
{
    protected Ingredient()
    {
    }

    public Ingredient(string name, string quantity)
    {
        Name = name;
        Quantity = quantity;
    }

	public string Name { get; private set; }
	public string Quantity { get; private set; }
    public Recipe Recipe { get; private set; }

    public void AddRecipe(Recipe recipe) => Recipe = recipe;
}
