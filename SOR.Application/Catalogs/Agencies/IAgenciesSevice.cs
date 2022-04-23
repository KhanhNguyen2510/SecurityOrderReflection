using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Agencies;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Agencies
{
    public interface IAgenciesSevice
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> CreateToAgencies(GetCreateToAgenciesRequest request);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateToAgencies(int Id, GetUpdateToAgenciesRequest request);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteToAgencies(int Id, CreateUserRequest request);

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetAgenciesViewModel> GetAgenciesById(int Id);
        Task<IEnumerable<GetAgenciesViewModel>> GetListToAgencies(GetMangagerToAgenciesRequest request);
        Task<PagedResult<GetAgenciesViewModel>> GetListPagingToAgencies(GetMangagerAgenciesRequest request);
    }
}
