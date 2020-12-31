using MarketplaceList.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceList.Infra.Mappings
{
    public class ShoppingListMap : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
             builder.ToTable("ShoppingList", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(1024)")
                .HasMaxLength(1024)
                .IsRequired();
            
            builder.Property(x => x.CreateAt)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.HasMany(x => x.Itens)
                   .WithOne(x => x.ShoppingList)
                   .HasForeignKey(e => e.ShoppingListId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new ShoppingList("compras para o churrasco"));
        }
    }
}