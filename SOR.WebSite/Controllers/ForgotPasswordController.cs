using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SOR.WebSite.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ILogger<ForgotPasswordController> _logger;

        public ForgotPasswordController(ILogger<ForgotPasswordController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
