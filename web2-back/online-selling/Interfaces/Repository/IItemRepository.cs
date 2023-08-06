using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Interfaces.Repository
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<Item> GetItemById(int id);
        Task DeleteItems(List<int> items);
        Task<List<Item>> GetItemsByUserId(UserIdDto userIdDto);
        Task<List<Item>> GetItemsById(List<int> ids);
    }
}
