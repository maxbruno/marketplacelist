using MarketplaceList.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketplaceList.Infra.Mappings
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
             builder.ToTable("Item", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(1024)")
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(x => x.Qtd)
                .HasColumnType("Int32")
                .IsRequired();
            
            builder.Property(x => x.CreateAt)
                .HasColumnType("DATETIME2")
                .IsRequired();
        }
    }
}