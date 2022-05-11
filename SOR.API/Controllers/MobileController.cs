using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Mobiles;
using SOR.Data.SystemBase;
using SOR.ViewModel;
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
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("add-token")]
        [SwaggerOperation(Summary = "Thêm token sau mỗi lần đăng nhập vào User")]
        public async Task<JsonResult> AddTokenInMobile([FromForm] GetAddToKenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.userId))
                    return Json(new ApiResponse(MessageBase.NO_CONTENT, 400));
                if (string.IsNullOrEmpty(request.Token))
                    return Json(new ApiResponse(MessageBase.NO_CONTENT, 400));

                request.userId = request.userId.Trim();
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
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPost("remove-token")]
        [SwaggerOperation(Summary = "Xóa token sau mỗi lần đăng xuất User")]
        public async Task<JsonResult> RemoveTokenInMobile([FromForm] CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.userId))
                    return Json(new ApiResponse(MessageBase.NO_CONTENT, 400));

                request.userId = request.userId.Trim();

                var data = await _mobileSevice.RemoveTokenInMobile(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Remove Token in Mobile: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Token in Mobile: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }
    }
}
