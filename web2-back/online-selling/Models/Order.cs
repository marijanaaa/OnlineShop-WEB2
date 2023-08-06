using online_selling.Enums;

namespace online_selling.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public OrderStatus Status { get; set; }

        public Order() { }
    }
}
