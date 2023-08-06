using online_selling.Enums;
using online_selling.Interfaces.DataInitialization;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Models;

namespace online_selling.Services.DataInitialization
{
    public class UserInitialization : IUserInitialization
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserInitialization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        void IUserInitialization.UserInitialization()
        {
            var result = _unitOfWork.Users.GetAllAsync().Result;
            if (result.Count == 0)
            {
                User user = new User("marijana", "marijanaaastojanovic@gmail.com", BCrypt.Net.BCrypt.HashPassword("marijana"),
                                    "Marijana", "Stojanovic", "2023 - 06 - 12T22: 00:00.000Z", "Valentina Vodnika", 0, "", null);
                _unitOfWork.Users.AddAsync(user);
                _unitOfWork.CompleteAsync();
            }
        }
    }
}
