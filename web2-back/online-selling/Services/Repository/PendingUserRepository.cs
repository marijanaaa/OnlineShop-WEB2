using Microsoft.EntityFrameworkCore;
using online_selling.Dto;
using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;
using online_selling.Models;

namespace online_selling.Services.Repository
{
    public class PendingUserRepository : GenericRepository<PendingUser>, IPendingUserRepository
    {
        public PendingUserRepository(OnlineShopDbContext context) : base(context)
        {
        }

        public async Task ApproveSellers(List<SelectedSellersDto> selectedSellersDtos)
        {
            var usernames = selectedSellersDtos.Select(p => p.Username).ToList();
            var emails = await _context.Users
                .Where(u => usernames.Contains(u.Username))
                .Select(u => u.Email)
                .ToListAsync();

            var entitiesToUpdate = await _context.PendingUsers
                .Where(x => emails.Contains(x.Email))
                .ToListAsync();

            foreach (var entity in entitiesToUpdate)
            {
                entity.Approved = true;
                entity.Pending = false;
            }

        }


        public async Task<List<User>> ListPendingUsers()
        {

            bool isTableEmpty = await _context.PendingUsers.AnyAsync();

            if (!isTableEmpty)
            {
                return new List<User>(); // or return null, throw an exception, or handle it as needed
            }

            var pendingUserEmails = await _context.PendingUsers
                .Where(p => p.Pending == true)
                .Select(p => p.Email)
                .ToListAsync();

            var users = await _context.Users
                .Where(u => pendingUserEmails.Contains(u.Email))
                .ToListAsync();

            return users;
        }

        public async Task RejectSellers(List<SelectedSellersDto> selectedSellersDtos)
        {
            var usernames = selectedSellersDtos.Select(p => p.Username).ToList();

            var emails = await _context.Users
                .Where(u => usernames.Contains(u.Username))
                .Select(u => u.Email)
                .ToListAsync();

            var entitiesToUpdate = await _context.PendingUsers
                .Where(x => emails.Contains(x.Email))
                .ToListAsync();

            foreach (var entity in entitiesToUpdate)
            {
                entity.Approved = false;
                entity.Pending = false;
            }

        }
    }
}
