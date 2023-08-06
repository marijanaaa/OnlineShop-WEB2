using online_selling.Enums;

namespace online_selling.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
