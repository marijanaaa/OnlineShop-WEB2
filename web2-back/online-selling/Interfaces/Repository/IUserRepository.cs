using online_selling.Dto;
using online_selling.Enums;
using online_selling.Models;

namespace online_selling.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        bool UsernameExist(string username);
        bool EmailExist(string email);
        Task<bool> Logout(string email);
        Task<User> GetUserById(int id);
        Task ChangeUsersStatus(List<User> userList, UserStatus userStatus);
        Task<List<User>> GetUsersByUsernames(List<SelectedSellersDto> selectedSellersDtos);
        Task<List<User>> GetAllUsers();
    }
}
