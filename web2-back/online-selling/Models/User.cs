using online_selling.Enums;
using System.Drawing;

namespace online_selling.Models
{
    public class User
    {
        public User(string username, string email, string password,string name,
            string lastname, string birthday, string address, UserType userType, string picture, UserStatus? userStatus)
        {
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            LastName = lastname;
            Birthday = birthday;
            Address = address;
            UserType = userType;
            Picture = picture;
            UserStatus = userStatus;
        }

        public User(string username) {
            Username = username;
        }
        public User() { }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        public UserType UserType { get; set; }  
        public string Picture { get; set; }
        public UserStatus? UserStatus { get; set; }
        public List<Item> Items { get; set; }
        public List<Order> Orders { get; set; }
    }
}
