using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Common;
using System;
using System.IO;
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
            try
            {
                var gReport = await GetAsync<PagedResult<GetReportViewModel>>($"/V1/user-panel/reports" +
    $"?userId={request.userId}&keyWord={request.keyWord}&isStatus={request.isStatus}&newslableId={request.newslableId}&isDate={request.isDate}&start={request.start}&end={request.end}" +
    $"&PageIndex={request.PageIndex}&PageSize={request.PageSize}");
                return gReport;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GetReportViewModel> GetReportByID(string Id)
        {
            try
            {
                var gReport = await GetAsync<GetReportViewModel>($"/V1/user-panel/reports/{Id}");
                return gReport;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ApiStatus> CreateReport(GetCreateToReportRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(!string.IsNullOrEmpty(request.title) ? request.title : ""), "title");
            requestContent.Add(new StringContent(request.content), "content");
            requestContent.Add(new StringContent(request.newsLabelId), "newsLabelId");
            requestContent.Add(new StringContent(request.iP), "iP");
            requestContent.Add(new StringContent(request.userAngel), "userAngel");
            requestContent.Add(new StringContent(request.locationReport), "locationReport");
            requestContent.Add(new StringContent(request.locationUser), "locationUser");
            if (request.files != null)
            {
                for (int i = 0; i < request.files.Count; i++)
                {
                    requestContent.Add(new StreamContent(request.files[i].OpenReadStream()), "files", Path.GetFileName(request.files[i].FileName));
                }
            }
            requestContent.Add(new StringContent(request.userId), "userId");

            var response = await client.PostAsync("/V1/user-panel/reports", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
        }

    }
}
