using Microsoft.EntityFrameworkCore;
using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;
using online_selling.Models;

namespace online_selling.Services.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OnlineShopDbContext context) : base(context)
        {
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            Order order = await _context.Orders.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return order;
        }
    }
}
