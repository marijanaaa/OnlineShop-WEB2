using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using online_selling.Models;

namespace online_selling.Infrastructure
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PendingUser> PendingUsers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public string MakeHash(string password) { 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineShopDbContext).Assembly);
            modelBuilder.Entity<User>().HasData(new User { Id = 21,Username = "marijana", Email="marijanaaa@gmail.com", 
                                                           Password = MakeHash("marijana"), Name="Marijana", LastName="Stojanovic",
                                                           Birthday = "2023-06-12T22:00:00.000Z", Address = "Valentina Vodnika", UserType=Enums.UserType.ADMIN, 
                                                           Picture="", UserStatus=null 
            });
        }
    }
}
