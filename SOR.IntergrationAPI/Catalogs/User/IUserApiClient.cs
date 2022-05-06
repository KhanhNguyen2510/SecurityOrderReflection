using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Users;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.User
{
    public interface IUserApiClient
    {
        Task<string> LoginInWed(GetLoginRequest request);
        Task<ApiResponse> Create(GetCreateToUserRequest request);
    }
}
