using AutoMapper;
using online_selling.Dto;
using online_selling.Interfaces.PendingUsers;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Models;
using System.Text;

namespace online_selling.Services.PendingUsers
{
    public class PendingUserService : IPendingUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PendingUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public static void SendEmail(string apiKey, string recipientEmail, string subject, string body)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.elasticemail.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();

                var requestContent = new StringContent(
                    $"apikey={Uri.EscapeDataString(apiKey)}&from=katarina.prodanovic20@gmail.com&to=" +
                    $"{Uri.EscapeDataString(recipientEmail)}&subject={Uri.EscapeDataString(subject)}&bodyHtml=" +
                    $"{Uri.EscapeDataString(body)}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"
                );

                var response = client.PostAsync("email/send", requestContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Email sent successfully!");
                }
                else
                {
                    var errorResponse = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Failed to send email. Error message: " + errorResponse);
                }
            }
        }
        public async Task<MessageDto> ApproveSellers(List<SelectedSellersDto> selectedSellersDtos)
        {
            await _unitOfWork.PendingUsers.ApproveSellers(selectedSellersDtos);
            //get users by username
            var users = await _unitOfWork.Users.GetUsersByUsernames(selectedSellersDtos);
            _unitOfWork.Users.ChangeUsersStatus(users, Enums.UserStatus.APPROVED);
            await _unitOfWork.CompleteAsync();
            var apiKey = "40EA2CA9DC23F13CE0581689714C527C836676DEF7F3CD3291EB6AF518CC51213A2B2BF92A539648DA897C7BE6A2C814";
            var recipientEmail = "katarina.prodanovic20@gmail.com";
            var subject = "Status of verification";
            var bodyHtml = "<h1>Hello,your status of verification is:</h1><p>Approved</p>";
            SendEmail(apiKey, recipientEmail, subject, bodyHtml);

            return new MessageDto { Message = "OK" };
        }

        public async Task<List<PendingDto>> ListPendingUsers()
        {
            var users = await _unitOfWork.PendingUsers.ListPendingUsers();
            var pendingDtos = _mapper.Map<List<PendingDto>>(users);
            return pendingDtos;
        }

        public async Task<MessageDto> RejectSellers(List<SelectedSellersDto> selectedSellersDtos)
        {
            await _unitOfWork.PendingUsers.RejectSellers(selectedSellersDtos);
            var users = await _unitOfWork.Users.GetUsersByUsernames(selectedSellersDtos);
            _unitOfWork.Users.ChangeUsersStatus(users, Enums.UserStatus.REJECTED);
            await _unitOfWork.CompleteAsync();
            var apiKey = "40EA2CA9DC23F13CE0581689714C527C836676DEF7F3CD3291EB6AF518CC51213A2B2BF92A539648DA897C7BE6A2C814";
            var recipientEmail = "katarina.prodanovic20@gmail.com";
            var subject = "Status of verification";
            var bodyHtml = "<h1>Hello,your status of verification is:</h1><p>Rejected</p>";
            SendEmail(apiKey, recipientEmail, subject, bodyHtml);
            return new MessageDto { Message = "OK" };
        }
    }
}
