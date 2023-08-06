using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using online_selling.Dto;
using online_selling.Interfaces.Authentication;
using online_selling.Interfaces.Tokens;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Models;
using System.Text;

namespace online_selling.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        private readonly IConfigurationSection _secretAccessKey;
        private readonly IConfigurationSection _secretRefreshKey;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Authentication(IConfiguration config, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _secretAccessKey = config.GetSection("SecretAccessKey");
            _secretRefreshKey = config.GetSection("SecretRefreshKey");
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(email);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public (string, string) MakeTokens(UserDto user)
        {
            ITokenMaker tokenMaker = new TokenMaker();
            SymmetricSecurityKey secretAccessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretAccessKey.Value));
            SymmetricSecurityKey secretRefreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretRefreshKey.Value));
            string accessToken;
            string refreshToken;
            (accessToken, refreshToken) = tokenMaker.MakeToken(user, secretAccessKey, secretRefreshKey);
            return (accessToken, refreshToken);
        }

        public async Task<MessageDto> Logout(string email)
        {
            var user = _unitOfWork.Users.GetUserByEmail(email);
            if (user == null) 
            {
                return new MessageDto { Message = "User doesn't exist to logout."};
            }
            await _unitOfWork.Users.Logout(email);
            await _unitOfWork.CompleteAsync();
            return new MessageDto { Message = "OK" };
        }

        public bool DoesUserExist(string email)
        {
            var user = _unitOfWork.Users.GetUserByEmail(email).Result;
            if (user == null) {
                return false;
            }
            return true;
        }

        public async Task<bool> IsAuthenticated(string email, string password)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(email);
            if (user == null) {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
            
        }

        //public bool IsPanding(User user)
        //{
        //    if (user.Pending == true)
        //        return true;
        //    return false;
        //}
    }
}
