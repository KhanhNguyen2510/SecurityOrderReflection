using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Proof;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Reports.Result;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Reports
{
    public interface IReportSevice
    {
        Task<ApiResponse> CreateToReport(GetCreateToReportRequest request);
        Task<ApiResponse> CreateToReportResult(GetCreateToReportResultRequest request);
        Task<ApiResponse> UpdateToReport(string Id, GetUpdateToReportRequest request);
        Task<ApiResponse> UpdateToReportResult(int Id, GetUpdateToReportResultRequest request);
        Task<ApiResponse> UpdateStatus(string Id, GetUpdateStatusInReportRequest request);
        Task<ApiResponse> UpdateIsReport(string Id, GetUpdateReportInReportRequest request);
        Task<ApiResponse> DeleteToReport(string Id, CreateUserRequest request);
        //Task<ApiResponse> DeleteToReportProof(int Id, CreateUserRequest request);
        Task<ApiResponse> DeleteToReportResult(int Id, CreateUserRequest request);
        Task<GetReportViewModel> GetReportById(string Id);
        Task<IEnumerable<GetReportViewModel>> GetListToReport(GetMangagerToReportRequest request);
        Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request);
    }
}
