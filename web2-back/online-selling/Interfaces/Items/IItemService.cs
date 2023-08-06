using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Interfaces.Items
{
    public interface IItemService
    {
        Task<MessageDto> AddItem(AddItemDto addItemDto);
        Task<MessageDto> UpdateItem(ItemDto item);
        Task DeleteItems(List<int> items);
        Task<List<ItemDto>> GetItemsByUserId(UserIdDto userIdDto);
        Task<ItemDto> GetItemById(int id);
        Task<List<ItemsForOrderDto>> GetAllItems();
    }
}
