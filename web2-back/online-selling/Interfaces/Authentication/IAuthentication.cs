using online_selling.Dto;
using online_selling.Models;
namespace online_selling.Interfaces.Authentication
{
    public interface IAuthentication
    {
        Task<UserDto> GetUserByEmail(string email);
        Task<bool> IsAuthenticated(string email, string password);
        //Boolean IsPanding(User user);
        (string, string) MakeTokens(UserDto user);
        Task<MessageDto> Logout(string email);
        Boolean DoesUserExist(string email);
    }
}
