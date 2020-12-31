using MarketplaceList.Domain.Models;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MarketplaceList.Infra.Repository
{
    public class ShoppingListRepository : EntityBaseRepository<ShoppingList>, IShoppingListRepository
    {
        private readonly EntityContext _context;
        public ShoppingListRepository(EntityContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<ShoppingList> GetByIdAsync(Guid id)
        {
            return await _context.ShoppingLists
                .AsNoTracking()
                .Include(x => x.Itens)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<ShoppingList>> GetAllAsync()
        {
            return await _context.ShoppingLists.ToListAsync();
        }
    }
}