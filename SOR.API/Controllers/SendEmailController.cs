using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.SendEmail;
using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.SendEmail;
using System;
using System.Threading.Tasks;

namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/admin-panel/send-emails")]
    public class SendEmailController : Controller
    {
        private readonly IMailService mailService;
        private readonly ILogger<SendEmailController> _logger;
        public SendEmailController(IMailService mailService, ILogger<SendEmailController> logger)
        {
            this.mailService = mailService;
            _logger = logger;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Send Email To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send Email To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Send Email To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send Email To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }
    }
}
