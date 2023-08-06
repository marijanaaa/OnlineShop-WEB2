using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using online_selling.Models;

namespace online_selling.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password);
            builder.Property(x => x.Name);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Birthday);
            builder.Property(x => x.Address);
            builder.Property(x => x.UserType);
            builder.HasMany<Item>(x => x.Items)
               .WithOne(i => i.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Order>(x => x.Orders)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
