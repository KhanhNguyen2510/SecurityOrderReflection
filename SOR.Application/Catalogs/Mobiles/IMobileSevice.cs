using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Mobiles
{
    public interface IMobileSevice
    {
        Task<ApiResponse> AddTokenInMobile(GetAddToKenRequest request);
        Task<ApiResponse> DeleteTokenInMobile(CreateUserRequest request);
        Task<ApiResponse> ShowNotificationInMobile(string userName, Notification request);
    }
}
