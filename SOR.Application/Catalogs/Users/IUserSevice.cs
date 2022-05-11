using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Users;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Users
{
    public interface IUserSevice
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Data.Entitis.User> Login(GetLoginRequest request);
        Task<string> LoginInWed(GetLoginRequest request);
        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Data.Entitis.User> CheckUser(string userName);
        Task<bool> UserNameExistence(string userName);
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> CreateToUser(GetCreateToUserRequest request);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateToUser(string userName, GetUpdateToUserRequest request);
        Task<ApiResponse> RoleUser(GetUpdateRoleToUserRequest request);
        Task<ApiResponse> UpdatePassWordToUser(string userName);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteToUse(string userName, CreateUserRequest request);
        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Data.Entitis.User> GetUserById(string userName);
        Task<IEnumerable<Data.Entitis.User>> GetListToUser(GetMangagerToUserRequest request);
        Task<PagedResult<Data.Entitis.User>> GetListPagingToUser(GetMangagerUserRequest request);
    }
}
