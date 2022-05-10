using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.IntergrationAPI.Catalogs.User;
using SOR.ViewModel.Catalogs.Users;
using System.Threading.Tasks;

namespace SOR.WebSite.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ILogger<ForgotPasswordController> _logger;
        private readonly IUserApiClient _userApiClient;

        public ForgotPasswordController(ILogger<ForgotPasswordController> logger, IUserApiClient userApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Reset(string userName, string PassWord, string NewPassWord)
        {
            var dUser = new GetUpdateToUserRequest()
            {
                PassWord = PassWord,
                NewPassWord = NewPassWord
            };
            var gUser = await _userApiClient.ResetPassword(userName, dUser);
            TempData["result"] = gUser.Message;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"].ToString();
            }
            return View();
        }
    }
}
