using online_selling.Models;

namespace online_selling.Interfaces.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderByUserId(int userId);
    }
}
