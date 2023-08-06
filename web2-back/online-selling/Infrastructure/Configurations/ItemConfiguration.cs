using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using online_selling.Models;

namespace online_selling.Infrastructure.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Price);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Description);
            builder.Property(x => x.Picture);
            builder.HasMany<OrderItem>(x => x.OrderItems)
                .WithOne(o => o.Item) // Assuming the Order class has a navigation property called "Items"
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User)
                .WithMany(o => o.Items)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
