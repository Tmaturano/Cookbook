namespace Cookbook.Domain.Entities;

public class Recipe : EntityBase
{
    public Recipe(string title, int category, string preparationMode)
    {
        Title = title;
        Category = category;
        PreparationMode = preparationMode;
    }

    protected Recipe()
	{
	}

	public string Title { get; private set; }
	public int Category { get; private set; }
	public string PreparationMode { get; private set; }
    public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient) => Ingredients.Add(ingredient);
    public void AddIngredients(IEnumerable<Ingredient> ingredients) => Ingredients.AddRange(ingredients);
}
