using online_selling.Dto;
using online_selling.Enums;
using online_selling.Models;

namespace online_selling.Interfaces.Users
{
    public interface IUserService
    {
        Task<MessageDto> Register(RegisterDto registerDto);
        Task<MessageDto> RegisterGoogle(string email, string name, string lastname, string picture);
        Task<UpdateDto> UpdateProfile (UpdateProfileDto updateProfileDto);
        Task<SellerStatusDto> GetSellerStatus(EmailDto sellerStatusDto);
        Task<List<UserDto>> GetAllUsers();
    }
}
