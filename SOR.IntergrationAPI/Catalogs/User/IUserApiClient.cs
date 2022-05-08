using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Users;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.User
{
    public interface IUserApiClient
    {
        Task<string> LoginInWed(GetLoginRequest request);
        Task<ApiStatus> Create(GetCreateToUserRequest request);
    }
}
