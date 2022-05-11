using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Users;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.User
{
    public interface IUserApiClient
    {
        Task<ApiStatus> ResetPassword(string userName, GetUpdateToUserRequest request);
        Task<string> LoginInWed(GetLoginRequest request);
        Task<ApiStatus> Create(GetCreateToUserRequest request);
        Task<ShowNotificationViewModel> NotificationInMobile(GetNotificationRequest request);
    }
}
