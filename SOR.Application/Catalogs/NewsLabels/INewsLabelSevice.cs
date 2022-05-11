using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.NewsLabels
{
    public interface INewsLabelSevice
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> CreateToNewsLable(GetCreateToNewsLableRequest request);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateToNewsLable(string Id, GetUpdateToNewsLableRequest request);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteToNewsLable(string Id, CreateUserRequest request);
        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetNewsLableViewModel> GetNewsLableById(string Id);
        Task<IEnumerable<GetNewsLableViewModel>> GetListToNewsLable(GetMangagerToNewsLableRequest request);
        Task<PagedResult<GetNewsLableViewModel>> GetListPagingToNewsLable(GetMangagerNewsLableRequest request);
        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Data.Entitis.NewsLabel> FindIdExistence(string Id);
    }
}
