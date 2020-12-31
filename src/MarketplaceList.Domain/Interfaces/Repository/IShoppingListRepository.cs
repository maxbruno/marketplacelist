using MarketplaceList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.Domain.Interfaces.Repository
{
    public interface IShoppingListRepository : IEntityBaseRepository<ShoppingList>
    {
        Task<ShoppingList> GetByIdAsync(Guid id);
        Task<ICollection<ShoppingList>> GetAllAsync();
    }
}