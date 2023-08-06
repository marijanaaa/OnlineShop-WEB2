using online_selling.Dto;

namespace online_selling.Interfaces.PendingUsers
{
    public interface IPendingUserService
    {
        Task<List<PendingDto>> ListPendingUsers();
        Task<MessageDto> ApproveSellers(List<SelectedSellersDto> selectedSellersDtos);
        Task<MessageDto> RejectSellers(List<SelectedSellersDto> selectedSellersDtos);
    }
}
