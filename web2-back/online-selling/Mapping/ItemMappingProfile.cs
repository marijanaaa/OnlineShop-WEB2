using AutoMapper;
using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Mapping
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<ItemsForOrderDto, Item>().ReverseMap();
        }
    }
}
