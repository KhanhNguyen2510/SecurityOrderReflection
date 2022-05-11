using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Users;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Users
{
    public interface IUserSevice
    {
        Task<Data.Entitis.User> CheckUser(string userName);
        Task<bool> UserNameExistence(string userName);
        Task<ApiResponse> UpdatePassWordToUser(string userName);
        Task<Data.Entitis.User> Login(GetLoginRequest request);
        Task<string> LoginInWed(GetLoginRequest request);
        Task<ApiResponse> CreateToUser(GetCreateToUserRequest request);
        Task<ApiResponse> UpdateToUser(string userName, GetUpdateToUserRequest request);
        Task<ApiResponse> RoleUser(GetUpdateRoleToUserRequest request);
        Task<ApiResponse> DeleteToUse(string userName, CreateUserRequest request);
        Task<Data.Entitis.User> GetUserById(string userName);
        Task<IEnumerable<Data.Entitis.User>> GetListToUser(GetMangagerToUserRequest request);
        Task<PagedResult<Data.Entitis.User>> GetListPagingToUser(GetMangagerUserRequest request);
    
    }
}
