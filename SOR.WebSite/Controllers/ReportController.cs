using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Data.Enum;
using SOR.IntergrationAPI.Catalogs.NewLables;
using SOR.IntergrationAPI.Catalogs.Reports;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Catalogs.Reports.Report;
using System;
using System.Threading.Tasks;

namespace SOR.WebSite.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportApiClient _reportApiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INewLableApiClient _newLableApiClient;

        public ReportController(ILogger<ReportController> logger, IReportApiClient reportApiClient, IHttpContextAccessor httpContextAccessor, INewLableApiClient newLableApiClient)
        {
            _reportApiClient = reportApiClient;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _newLableApiClient = newLableApiClient;
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(string keyWord)
        {
            var dNewLable = new GetMangagerToNewsLableRequest()
            {
                keyWord = keyWord
            };
            var gNewLable = await _newLableApiClient.GetListToNewsLable(dNewLable);

            ViewData["newlable"] = gNewLable;
           return View();
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = 737280000)]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(737280000)]
        public async Task<IActionResult> Create([FromForm] GetCreateToReportRequest request )
        {
            var userAgent = Request.Headers["User-Agent"];
            var ipAdress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";

            var dReport = new GetCreateToReportRequest()
            {
                content = request.content,
                files = request.files,
                iP = ipAdress,
                locationReport = request.locationReport,
                locationUser = request.locationUser,
                newsLabelId =request.newsLabelId,
                title = request.title,
                userAngel = userAgent,
                userId = User.Identity.Name
            };

            await _reportApiClient.CreateReport(dReport);

            return RedirectToAction("Create");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> HistoryReport(string keyWord, IsStatus? isStatus, string newslableId, DateTime? end, DateTime? start, IsDate? isDate, int pageIndex = 1, int PageSize = 100)
        {
            var dReport = new GetMangagerReportRequest()
            {
                keyWord = keyWord,
                isStatus = isStatus,
                newslableId = newslableId,
                PageIndex = pageIndex,
                PageSize = PageSize,
                end = end,
                start = start,
                isDate = isDate,
                userId = User.Identity.Name
            };

            var gReport = await _reportApiClient.GetListPagingToReport(dReport);
            return View(gReport);
        }
    }
}
