using Cookbook.SharedKernel.Enum;

namespace Cookbook.Domain.Entities;

public class Recipe : EntityBase
{
    public Recipe(string title, Category category, string preparationMode)
    {
        Title = title;
        Category = category;
        PreparationMode = preparationMode;
    }

    protected Recipe()
	{
	}

    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
	public Category Category { get; private set; }
	public string PreparationMode { get; private set; }
    public ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();
    public User Owner { get; private set; }

    public void AddIngredient(Ingredient ingredient) => Ingredients.Add(ingredient);
    public void AddIngredients(IEnumerable<Ingredient> ingredients) => Ingredients.ToList().AddRange(ingredients);
    public void AddOwner(User owner) => Owner = owner;
    public void SetOwnerId(Guid ownerId) => OwnerId = ownerId;
}
