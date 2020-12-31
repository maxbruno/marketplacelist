using FluentValidation;
using MarketplaceList.Domain.Models;

namespace MarketplaceList.Domain.Validations
{
    public class ShoppingListDeleteValidation : AbstractValidator<ShoppingList>
    {
        public ShoppingListDeleteValidation()
        {
            RuleFor(x => x.Id)
                 .NotNull()
                 .WithMessage("Id não pode ser nulo");
        }
    }
}
