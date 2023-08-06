using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<ReturnOrderDto> AddOrder(OrderDto order);
    }
}
