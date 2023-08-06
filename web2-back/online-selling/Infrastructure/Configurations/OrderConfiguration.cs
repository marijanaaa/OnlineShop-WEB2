using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using online_selling.Models;

namespace online_selling.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Comment);
            builder.Property(x => x.Address);
            builder.Property(x => x.Price);
            builder.Property(x => x.Status);


            builder.HasMany<OrderItem>(x => x.OrderItems)
               .WithOne(i => i.Order)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>(x => x.User)
                .WithMany(i => i.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
