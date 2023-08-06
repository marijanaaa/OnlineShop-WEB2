namespace online_selling.Models
{
    public class PendingUser
    {
        public PendingUser(string email, bool approved, bool pending)
        {
            Email = email;
            Approved = approved;
            Pending = pending;
        }

        public PendingUser() { }

        public int Id { get; set; } 
        public string Email { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
    }
}
