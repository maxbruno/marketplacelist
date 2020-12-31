using FluentValidation;
using MarketplaceList.Domain.Models;

namespace MarketplaceList.Domain.Validations
{
    public class ShoppingListInsertValidation : AbstractValidator<ShoppingList>
    {
        public ShoppingListInsertValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("O nome da lista não pode ser nulo.");
        }
    }
}
