using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Historys;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Historys
{
    public interface IHistorySevice
    {
        Task<ApiResponse> CreateToHistory(GetCreateToHistoryRequest request, int userId);
        Task<ApiResponse> UpdateToHistory(int Id, GetUpdateToHistoryRequest request, int userId);
        Task<ApiResponse> DeleteToAgencies(int Id, int userId);
    }
}
