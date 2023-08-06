using AutoMapper;
using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, PendingDto>().ReverseMap();
            CreateMap<User, UpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<SelectedSellersDto, User>().ReverseMap();
        }
    }
}
