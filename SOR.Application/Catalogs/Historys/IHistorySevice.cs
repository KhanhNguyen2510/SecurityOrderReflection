using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Historys
{
    public interface IHistorySevice
    {
        Task<ApiResponse> CreateToHistory(GetCreateToHistoryRequest request);
        Task<ApiResponse> UpdateToHistory(int Id, GetUpdateToHistoryRequest request);
        Task<ApiResponse> DeleteToHistory(int Id, CreateUserRequest request);
        Task<GetHistoryViewModel> GetHistoryById(int Id);
        Task<IEnumerable<GetHistoryViewModel>> GetListToHistory(GetMangagerToHistoryRequest request);
        Task<PagedResult<GetHistoryViewModel>> GetListPagingToHistory(GetMangagerHistoryRequest request);
    }
}
