using Microsoft.EntityFrameworkCore;
using online_selling.Dto;
using online_selling.Infrastructure;
using online_selling.Interfaces.Repository;
using online_selling.Models;

namespace online_selling.Services.Repository
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(OnlineShopDbContext context) : base(context)
        {
        }

    
        public async Task DeleteItems(List<int> items)
        {
            var itemsToDelete = await _context.Items
                .Where(item => items.Contains(item.Id))
                .ToListAsync();

            _context.Items.RemoveRange(itemsToDelete);
        }

        public async Task<Item> GetItemById(int id)
        {
            var result = await _context.Items.FirstOrDefaultAsync(x => Equals(x.Id, id));
            return result;
        }

        public async Task<List<Item>> GetItemsById(List<int> ids)
        {
            var items = await _context.Items
                .Where(item => ids.Contains(item.Id))
                .ToListAsync();

            return items;
        }

        public async Task<List<Item>> GetItemsByUserId(UserIdDto userIdDto)
        {
            int userId = userIdDto.Id;
            var items = await _context.Items.Where(item => item.UserId == userId).ToListAsync();
            return items;
        }
    }
}
