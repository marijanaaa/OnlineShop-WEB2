using Microsoft.EntityFrameworkCore;
using online_selling.Dto;
using online_selling.Enums;
using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;
using online_selling.Models;
using System.Linq;

namespace online_selling.Services.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(OnlineShopDbContext onlineShop) : base(onlineShop)
        {

        }

        public bool EmailExist(string email)
        {
            var result = _context.Users.SingleOrDefault(u => u.Email == email);
            if (result == null) return false;
            return true;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var result =  await _context.Users.FirstOrDefaultAsync(x=>Equals(x.Email, email));
            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => Equals(x.Id, id));
            return result;
        }

        public async Task ChangeUsersStatus(List<User> userList, UserStatus userStatus)
        {
            var users = await _context.Users
               .Where(u => userList.Select(user => user.Username).Contains(u.Username))
               .ToListAsync();

            foreach (var user in users)
            {
                user.UserStatus = userStatus;
            }
        }

        public async Task<bool> Logout(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var res = _context.Users.Update(user).Entity;
            if (res != null)
                return true;
            return false;
        }

        public bool UsernameExist(string username)
        {
            var result = _context.Users.SingleOrDefault(u => u.Username == username);
            if (result == null) return false;
            return true;
        }

        public async Task<List<User>> GetUsersByUsernames(List<SelectedSellersDto> selectedSellersDtos)
        {
            var usernames = selectedSellersDtos.Select(p => p.Username).ToList();
            var users = await _context.Users
                .Where(u => u.Username != null && usernames.Contains(u.Username))
                .ToListAsync();
            return users;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var result = await _context.Users.Where(user => user.UserStatus != null).ToListAsync();
            return result;
        }
    }
}
