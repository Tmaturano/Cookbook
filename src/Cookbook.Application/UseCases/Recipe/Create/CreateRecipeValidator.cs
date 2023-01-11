using Cookbook.Communication.Request;
using FluentValidation;

namespace Cookbook.Application.UseCases.Recipe.Create;

public class CreateRecipeValidator : AbstractValidator<RegisterRecipeRequest>
{
    public CreateRecipeValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.PreparationMode).NotEmpty();
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.Ingredients).NotEmpty();
        RuleForEach(x => x.Ingredients).ChildRules(ingredient =>
        {
            ingredient.RuleFor(y => y.Name).NotEmpty();
            ingredient.RuleFor(y => y.Quantity).NotEmpty();
        });

        RuleFor(x => x.Ingredients).Custom((ingredients, context) =>
        {
            var distinctProducts = ingredients.Select(c => c.Name).Distinct();

            //check for duplicated ingredients
            if (distinctProducts.Count() != ingredients.Count)
                context.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredients", ""));
        });
    }
}
