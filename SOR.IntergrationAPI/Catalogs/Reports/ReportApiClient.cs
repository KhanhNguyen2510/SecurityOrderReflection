using Microsoft.Extensions.Configuration;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.Reports
{
    public class ReportApiClient : BaseApiClient , IReportApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ReportApiClient(IHttpClientFactory httpClientFactory,
                   IConfiguration configuration)
           : base(httpClientFactory, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request)
        {
            var gReport = await GetAsync<PagedResult<GetReportViewModel>>($"/V1/user-panel/reports?keyWord={request.keyWord}&IsStatus={request.IsStatus}" +
                $"&NewslableId={request.NewslableId}" +
                $"&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
            return gReport;
        }

    }
}
