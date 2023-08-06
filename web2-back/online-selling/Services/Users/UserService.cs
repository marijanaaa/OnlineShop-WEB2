using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using online_selling.Dto;
using online_selling.Enums;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Interfaces.Users;
using online_selling.Models;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace online_selling.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SellerStatusDto> GetSellerStatus(EmailDto sellerStatusDto)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(sellerStatusDto.Email);
            SellerStatusDto statusDto = new SellerStatusDto();
            statusDto.UserStatus = user.UserStatus;
            return statusDto;
        }

        public async Task<MessageDto> Register(RegisterDto registerDto)
        {
            if (string.IsNullOrEmpty(registerDto.Username) || _unitOfWork.Users.UsernameExist(registerDto.Username))
            {
                return new MessageDto { Message = "Username is empty or already exists." };
            }
            if (string.IsNullOrEmpty(registerDto.Email) || _unitOfWork.Users.EmailExist(registerDto.Email))
            {
                return new MessageDto { Message = "Email is empty or already exists." };
            }
            if (string.IsNullOrEmpty(registerDto.Password))
            {
                return new MessageDto { Message = "Password is empty." };
            }
            if (string.IsNullOrEmpty(registerDto.ConfirmPassword))
            {
                return new MessageDto { Message = "Confirm password is empty." };
            }
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return new MessageDto { Message = "Your confirm password is incorrect." };
            }
            if (string.IsNullOrEmpty(registerDto.Name))
            {
                return new MessageDto { Message = "Name is empty." };
            }
            if (string.IsNullOrEmpty(registerDto.Lastname))
            {
                return new MessageDto { Message = "Lastname is empty." };
            }
            //datetime validation
            if (DateTime.TryParse(registerDto.Birthday, out DateTime parsedBirthday))
            {
                // Check if the date falls within an appropriate range (e.g. not in the future or too far in the past)
                if (parsedBirthday >= DateTime.Now && parsedBirthday <= DateTime.Now.AddYears(-120))
                {
                    return new MessageDto { Message = "Birthday is not valid." };
                }
            }
            else {
                return new MessageDto { Message = "Birthday is not valid." };
            }

            if (string.IsNullOrEmpty(registerDto.Address))
            {
                return new MessageDto { Message = "Address is empty." };
            }

            //enum validation 
            UserType userType;
            if(!Enum.TryParse(registerDto.UserType, true, out userType)) {
                return new MessageDto { Message = "User type is not valid." };
            }
            

            User user = new User();
            user.Username = registerDto.Username;
            user.Email = registerDto.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.Name = registerDto.Name;
            user.LastName = registerDto.Lastname;
            user.Birthday = registerDto.Birthday;
            user.Address = registerDto.Address;
            user.Picture = registerDto.Picture;
            if(registerDto.UserType == "ADMIN") user.UserType = Enums.UserType.ADMIN;
            else if(registerDto.UserType == "BUYER") user.UserType = Enums.UserType.BUYER;
            else if(registerDto.UserType == "SELLER") user.UserType = Enums.UserType.SELLER;

            if (user.UserType == Enums.UserType.SELLER)
            {
                user.UserStatus = UserStatus.PENDING;
                //adding in Pending table
                PendingUser pendingUser = new PendingUser(user.Email, false, true);
                await _unitOfWork.PendingUsers.AddAsync(pendingUser);
                await _unitOfWork.Users.AddAsync(user); 
                await _unitOfWork.CompleteAsync();
                return new MessageDto { Message = "OK" };
            }
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new MessageDto { Message = "OK" };

        }

        public async Task<MessageDto> RegisterGoogle(string email, string name, string lastname, string picture)
        {
            User user = new User() { Email = email, Name = name, LastName = lastname, Picture = picture};
            user.UserType = Enums.UserType.BUYER;
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new MessageDto { Message = "OK" };
        }

        public async Task<UpdateDto> UpdateProfile(UpdateProfileDto updateProfileDto)
        {
            //validacije trebaju ako se mijenja username ili email
            User existingUser = await _unitOfWork.Users.GetUserById(updateProfileDto.Id);
            if (updateProfileDto.Username != null && updateProfileDto.Username != existingUser.Username) {
                bool usernameExists = _unitOfWork.Users.UsernameExist(updateProfileDto.Username);
                if (!usernameExists) {
                    existingUser.Username = updateProfileDto.Username;
                }
            }
            if (updateProfileDto.Email != null && updateProfileDto.Email != existingUser.Email)
            {
                bool emailExists = _unitOfWork.Users.EmailExist(updateProfileDto.Email);
                if (!emailExists) {
                    existingUser.Email = updateProfileDto.Email;
                }
            }
            if (updateProfileDto.OldPassword != null && updateProfileDto.ChangedPassword != null && updateProfileDto.ConfirmPassword != null)                         
            {
                if (BCrypt.Net.BCrypt.Verify(BCrypt.Net.BCrypt.HashPassword(updateProfileDto.OldPassword), existingUser.Password)) {
                    if (updateProfileDto.ChangedPassword == updateProfileDto.ConfirmPassword) {
                        existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateProfileDto.ConfirmPassword);
                    }
                }
            }
            if (updateProfileDto.Name != null && updateProfileDto.Name != existingUser.Email) { 
                existingUser.Name = updateProfileDto.Name;
            }
            if (updateProfileDto.Lastname != null && updateProfileDto.Lastname != existingUser.LastName) {
                existingUser.LastName = updateProfileDto.Lastname;
            }
            if (updateProfileDto.Birthday != null && updateProfileDto.Birthday != existingUser.Birthday) { 
                existingUser.Birthday = updateProfileDto.Birthday;
            }
            if (updateProfileDto.Address != null && updateProfileDto.Address != existingUser.Address) { 
                existingUser.Address = updateProfileDto.Address;
            }
            if (updateProfileDto.Picture != null && updateProfileDto.Picture != existingUser.Picture) {
                existingUser.Picture = updateProfileDto.Picture;
            }

            var result = _unitOfWork.Users.UpdateSync(existingUser);
            await _unitOfWork.CompleteAsync();
            var updatedUsers = _mapper.Map<UpdateDto>(result);
            return updatedUsers;

        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var result = await _unitOfWork.Users.GetAllUsers();
            var usersDto = _mapper.Map<List<UserDto>>(result);
            return usersDto;
        }
    }
}
