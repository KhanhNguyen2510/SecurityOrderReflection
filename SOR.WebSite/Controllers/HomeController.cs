﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Data.Enum;
using SOR.IntergrationAPI.Catalogs.NewLables;
using SOR.IntergrationAPI.Catalogs.Reports;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SOR.WebSite.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewLableApiClient _newLableApiClient;
        private readonly IReportApiClient _reportApiClient;

        public HomeController(ILogger<HomeController> logger, INewLableApiClient newLableApiClient, IReportApiClient reportApiClient)
        {
            _newLableApiClient = newLableApiClient;
            _logger = logger;
            _reportApiClient = reportApiClient;
        }
        [Authorize]
        [HttpGet]
        public  async Task<IActionResult> Index(string keyWord , IsStatus? isStatus , string newslableId , int pageIndex = 1 , int PageSize = 100)
        {
            //var user = User.Identity.Name;
            //var ss = User.Claims.Skip(1).FirstOrDefault().Value;

            var dReport = new GetMangagerReportRequest()
            {
                keyWord = keyWord,
                IsStatus =  isStatus,
                NewslableId = newslableId,
                PageIndex = pageIndex,
                PageSize = PageSize 
            };

            var gReport = await _reportApiClient.GetListPagingToReport(dReport);

            return  View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
