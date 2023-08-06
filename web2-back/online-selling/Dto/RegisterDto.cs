using online_selling.Enums;

namespace online_selling.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string Picture { get; set; }
        public RegisterDto() { }
    }
}
