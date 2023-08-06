using online_selling.Enums;

namespace online_selling.Dto
{
    public class LoginDto
    {
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public LoginDto() { }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
