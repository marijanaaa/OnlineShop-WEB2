namespace online_selling.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public Item() { }

        public Item(string name, int price, int amount, string description, string picture)
        {
            Name = name;
            Price = price;
            Amount = amount;
            Description = description;
            Picture = picture;
        }
    }
}
