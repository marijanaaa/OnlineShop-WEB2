namespace online_selling.Dto
{
    public class OrderDto
    {
        public Dictionary<int, int> ItemIds { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
    }
}
