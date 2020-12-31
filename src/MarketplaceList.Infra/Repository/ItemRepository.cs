using MarketplaceList.Domain.Models;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MarketplaceList.Infra.Repository
{
    public class ItemRepository : EntityBaseRepository<Item>, IItemRepository
    {
        private readonly EntityContext _context;
        public ItemRepository(EntityContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Item>> GetAllAsync(Guid shoppingListId)
        {
            return await _context.Itens
                .Where(x => x.ShoppingListId == shoppingListId)
                .ToListAsync();
        }

        public async Task<Item> GetByIdAsync(Guid id)
        {
            return await _context.Itens
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}