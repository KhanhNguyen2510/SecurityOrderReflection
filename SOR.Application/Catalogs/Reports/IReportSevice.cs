using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Reports.Result;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Reports
{
    public interface IReportSevice
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> CreateToReport(GetCreateToReportRequest request);
        Task<ApiResponse> CreateToReportResult(GetCreateToReportResultRequest request);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateToReport(string Id, GetUpdateToReportRequest request);
        Task<ApiResponse> UpdateToReportResult(int Id, GetUpdateToReportResultRequest request);
        Task<ApiResponse> UpdateStatus(string Id, GetUpdateStatusInReportRequest request);
        Task<ApiResponse> UpdateIsReport(string Id, GetUpdateReportInReportRequest request);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ApiResponse> DeleteToReport(string Id, CreateUserRequest request);
        Task<ApiResponse> DeleteToReportResult(int Id, CreateUserRequest request);
        /// <summary>
        /// List
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GetReportViewModel> GetReportById(string Id);
        Task<IEnumerable<GetReportViewModel>> GetListToReport(GetMangagerToReportRequest request);
        Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request);
    }
}
