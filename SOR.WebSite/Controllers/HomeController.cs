using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.IntergrationAPI.Catalogs.NewLables;
using SOR.ViewModel.Catalogs.NewLables;
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

        public HomeController(ILogger<HomeController> logger, INewLableApiClient newLableApiClient)
        {
            _newLableApiClient = newLableApiClient;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string keyWord)
        {
            //var user = User.Identity.Name;
            //var ss = User.Claims.Skip(1).FirstOrDefault().Value;

            var dNewLable = new GetMangagerToNewsLableRequest()
            {
                keyWord = keyWord
            };

            var gNewLables = await _newLableApiClient.GetListToNewsLable(dNewLable);

            ViewBag.GetNewLables = gNewLables;

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
