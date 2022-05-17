﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.IntergrationAPI.Catalogs.NewLables;
using SOR.IntergrationAPI.Catalogs.Reports;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Users;
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
            var ipAdress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var h = new GetCreateToReportRequest()
            {
                content = request.content,
                files = request.files,
                iP = ipAdress,
                locationReport = request.locationReport,
                locationUser = request.locationUser,
                newsLabelId = "hinhsu",
                title = request.title,
                userAngel = userAgent,
                userId = User.Identity.Name
            };

            var gReport = await _reportApiClient.CreateReport(h);
            return View();
        }

    }
}
