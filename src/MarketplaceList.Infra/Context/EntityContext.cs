using MarketplaceList.Domain.Models;
using MarketplaceList.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceList.Infra.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
             : base(options) { }

        public DbSet<Item> Itens { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemMap());
            modelBuilder.ApplyConfiguration(new ShoppingListMap());
        }
    }
}