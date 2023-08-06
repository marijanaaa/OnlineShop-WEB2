using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;
using online_selling.Interfaces.UnitOfWork;

namespace online_selling.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext _context;
        public IUserRepository Users { get; private set; }
        public IPendingUserRepository PendingUsers { get; private set; }
        public IItemRepository Items { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public UnitOfWork(OnlineShopDbContext context, IUserRepository users, IPendingUserRepository pendingUsers, IItemRepository items, IOrderRepository orders) {
            _context = context;     
            Users = users;
            PendingUsers = pendingUsers;
            Items = items;
            Orders = orders;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void CompleteSync()
        {
            _context.SaveChanges();
        }
    }
}
