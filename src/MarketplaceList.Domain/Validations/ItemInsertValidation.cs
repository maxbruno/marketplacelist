using FluentValidation;
using MarketplaceList.Domain.Models;

namespace MarketplaceList.Domain.Validations
{
    public class ItemInsertValidation : AbstractValidator<Item>
    {
        public ItemInsertValidation()
        {

        }
    }
}
