using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Mobiles;
using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Mobile;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/user-panel/mobiles")]
    public class MobileController :Controller
    {
        private readonly IMobileSevice _mobileSevice;
        private readonly ILogger<MobileController> _logger;

        public MobileController(ILogger<MobileController> logger, IMobileSevice mobileSevice)
        {
            _logger = logger;
            _mobileSevice = mobileSevice;
        }

        [HttpPost("asdasd")]
        [SwaggerOperation(Summary = "Kiểm tra trùng trùng tài khoản user")]
        public async Task<JsonResult> CheckUserNasdsadame(string userName,[FromForm]Notification request)
        {
            try
            {
                var data = await _mobileSevice.ShowNotificationInMobile(userName,request);
                return Json(data); 
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Check UserName To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Check UserName To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpPost("token")]
        [SwaggerOperation(Summary = "Thêm token sau mỗi lần đăng nhập vào User")]
        public async Task<JsonResult> AddTokenInMobile([FromForm] GetAddToKenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserName))
                    return Json(new ApiResponse(MessageBase.NO_CONTENT, 400));
                if (string.IsNullOrEmpty(request.Token))
                    return Json(new ApiResponse(MessageBase.NO_CONTENT, 400));

                request.UserName = request.UserName.Trim();
                request.Token = request.Token.Trim();

                var data = await _mobileSevice.AddTokenInMobile(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Add Token in Mobile: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add Token in Mobile: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }
    }
}
