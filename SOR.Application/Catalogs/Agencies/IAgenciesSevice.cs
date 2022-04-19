using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Agencies;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Agencies
{
    public interface IAgenciesSevice
    {
        Task<ApiResponse> CreateToAgencies(GetCreateToAgenciesRequest request, int userId);
        Task<ApiResponse> UpdateToAgencies(int Id, GetUpdateToAgenciesRequest request, int userId);
        Task<ApiResponse> DeleteToAgencies(int Id, int userId);
    }
}
