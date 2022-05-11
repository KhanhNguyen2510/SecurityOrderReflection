using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Historys
{
    public interface IHistorySevice
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> CreateToHistory(GetCreateToHistoryRequest request);

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetHistoryViewModel> GetHistoryById(int Id);
        Task<IEnumerable<GetHistoryViewModel>> GetListToHistory(GetMangagerToHistoryRequest request);
        Task<PagedResult<GetHistoryViewModel>> GetListPagingToHistory(GetMangagerHistoryRequest request);
    }
}
