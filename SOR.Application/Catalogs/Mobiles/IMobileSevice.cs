using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Mobiles
{
    public interface IMobileSevice
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> AddTokenInMobile(GetAddToKenRequest request);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> RemoveTokenInMobile(CreateUserRequest request);
        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> ShowNotificationInMobile(string userName, Notification request);
    }
}
