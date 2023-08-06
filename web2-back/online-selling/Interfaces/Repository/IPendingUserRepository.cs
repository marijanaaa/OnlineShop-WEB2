using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Interfaces.Repository
{
    public interface IPendingUserRepository: IGenericRepository<PendingUser>
    {
        Task<List<User>> ListPendingUsers();
        Task ApproveSellers(List<SelectedSellersDto> selectedSellersDtos);
        Task RejectSellers(List<SelectedSellersDto> selectedSellersDtos);
    }
}
