using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Common;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.Reports
{
    public interface IReportApiClient
    {
        Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request);
        Task<GetReportViewModel> GetReportByID(string Id);
        Task<ApiStatus> CreateReport(GetCreateToReportRequest request);
    }
}
