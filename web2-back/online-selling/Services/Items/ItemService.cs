using AutoMapper;
using online_selling.Dto;
using online_selling.Interfaces.Items;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Models;

namespace online_selling.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MessageDto> AddItem(AddItemDto itemDto)
        {
            Item item = new Item(itemDto.Name, itemDto.Price, itemDto.Amount, itemDto.Description, itemDto.Picture);
            item.UserId = itemDto.UserId;
            //treba naci usera sa tim idjem 
            User user = await _unitOfWork.Users.GetUserById(itemDto.UserId);
            item.User = user;
            await _unitOfWork.Items.AddAsync(item);
            await _unitOfWork.CompleteAsync();
            return new MessageDto { Message = "OK"};
        }

        public async Task DeleteItems(List<int> items)
        {
            await _unitOfWork.Items.DeleteItems(items);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<ItemsForOrderDto>> GetAllItems()
        {
            var result = await _unitOfWork.Items.GetAllAsync();
            var items = _mapper.Map<List<ItemsForOrderDto>>(result);
            return items;
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            Item item = await _unitOfWork.Items.GetItemById(id);
            var itemDto = _mapper.Map<ItemDto>(item);
            return itemDto;
        }

        public async Task<List<ItemDto>> GetItemsByUserId(UserIdDto userIdDto)
        {
            var result = await _unitOfWork.Items.GetItemsByUserId(userIdDto); 
            var items = _mapper.Map<List<ItemDto>>(result);
            return items;   
        }

        public async Task<MessageDto> UpdateItem(ItemDto item)
        {
            var existingItem = await _unitOfWork.Items.GetItemById(item.Id);
            if (item.Name != null && item.Name != existingItem.Name)
            {
                existingItem.Name = item.Name;
            }
            if (item.Price != null && item.Price != existingItem.Price) { 
                existingItem.Price = item.Price;
            }
            if (item.Amount != null && item.Amount != existingItem.Amount) { 
                existingItem.Amount = item.Amount;
            }
            if (item.Description != null && item.Description != existingItem.Description) { 
                existingItem.Description = item.Description;
            }
            if(item.Picture != null && item.Picture != existingItem.Picture) { 
                existingItem.Picture = item.Picture;
            }

            var result = _unitOfWork.Items.UpdateSync(existingItem);
            await _unitOfWork.CompleteAsync();
            return new MessageDto { Message = "OK"};
        }
    }
}
