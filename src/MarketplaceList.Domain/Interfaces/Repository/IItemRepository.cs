using MarketplaceList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.Domain.Interfaces.Repository
{
    public interface IItemRepository : IEntityBaseRepository<Item>
    {
        Task<Item> GetByIdAsync(Guid id);
        Task<ICollection<Item>> GetAllAsync(Guid shoppingListId);
    }
}