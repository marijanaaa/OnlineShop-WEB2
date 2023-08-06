using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using online_selling.Models;

namespace online_selling.Infrastructure.Configurations
{
    public class PendingUserConfiguration : IEntityTypeConfiguration<PendingUser>
    {
        public void Configure(EntityTypeBuilder<PendingUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Approved);
        }
    }
}
