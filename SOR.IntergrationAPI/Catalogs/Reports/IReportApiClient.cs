using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Common;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.Reports
{
    public interface IReportApiClient
    {
        Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request);
    }
}
