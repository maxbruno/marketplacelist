using FluentValidation;
using MarketplaceList.Domain.Models;

namespace MarketplaceList.Domain.Validations
{
    public class ItemDeleteValidation : AbstractValidator<Item>
    {
        public ItemDeleteValidation()
        {
            RuleFor(x => x.Id)
                 .NotNull()
                 .WithMessage("Id não pode ser nulo");
        }
    }
}
