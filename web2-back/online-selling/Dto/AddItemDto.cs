namespace online_selling.Dto
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int UserId { get; set; }
    }
}
