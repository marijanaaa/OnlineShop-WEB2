using online_selling.Interfaces.Repository;

namespace online_selling.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPendingUserRepository PendingUsers { get; }
        IItemRepository Items { get; }
        IOrderRepository Orders { get; }
        
        Task CompleteAsync();
        void CompleteSync();
    }
}
