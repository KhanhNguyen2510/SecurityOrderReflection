using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Users;
using SOR.Data.SystemBase;
using SOR.IntergrationAPI.Catalogs.User;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Users;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/admin-panel/users")]
    public class UserController : Controller
    {

        private readonly IUserSevice _userSevice;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserSevice userSevice)
        {
            _logger = logger;
            _userSevice = userSevice;
        }

        [HttpGet("check-username")]
        [SwaggerOperation(Summary = "Kiểm tra trùng trùng tài khoản user")]
        public async Task<JsonResult> CheckUserName(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return Json(false);
                userName = userName.Trim();
                var data = await _userSevice.UserNameExistence(userName);
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

        [HttpPost("reset-password")]
        [SwaggerOperation(Summary = "Tạo lại mật khẩu")]
        public async Task<JsonResult> ResetPassWord(string userName)
        {
            try
            {
                var data = await _userSevice.UpdatePassWordToUser(userName);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Reset PassWord To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reset PassWord To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Đăng nhập")]
        public async Task<JsonResult> Login([FromForm] GetLoginRequest request)
        {
            try
            {
                var data = await _userSevice.Login(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Login To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpPost("login-in-web")]
        [SwaggerOperation(Summary = "Đăng nhập trên Website")]
        public async Task<JsonResult> LoginInWeb([FromForm] GetLoginRequest request)
        {
            try
            {
                var data = await _userSevice.LoginInWed(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Login To User In WebSite: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login To User In WebSite: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Thêm tài khoản người dùng")]
        public async Task<JsonResult> Create([FromForm] GetCreateToUserRequest request)
        {
            try
            {
                var data = await _userSevice.CreateToUser(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{userName}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin tài khoản người dùng")]
        public async Task<JsonResult> Update(string userName,[FromForm] GetUpdateToUserRequest request)
        {
            try
            {
                var data = await _userSevice.UpdateToUser(userName, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpDelete("{userName}")]
        [SwaggerOperation(Summary = "Xóa thông tin tài khoản người dùng")]
        public async Task<JsonResult> Delete(string userName, [FromForm]CreateUserRequest request)
        {
            try
            {
                var data = await _userSevice.DeleteToUse(userName, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpGet("{userName}/get-id")]
        [SwaggerOperation(Summary = "Hiển thị thông tin người dùng theo tên UserName")]
        public async Task<JsonResult> GetById(string userName)
        {
            try
            {
                var data = await _userSevice.GetUserById(userName);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get By UserName {userName} To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get By UserName {userName} To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpGet("list-users")]
        [SwaggerOperation(Summary = "Hiển thị tất cả thông tin tài khoản")]
        public async Task<JsonResult> List([FromQuery] GetMangagerToUserRequest request)
        {
            try
            {
                var data = await _userSevice.GetListToUser(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hiển thị thông tin tài khoản có phân trang")]
        public async Task<JsonResult> ListPaging([FromQuery] GetMangagerUserRequest request)
        {
            try
            {
                var data = await _userSevice.GetListPagingToUser(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List Paging To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List Paging To User: Message:{ex.Message}");
                throw new ApiException(ex.Message);
            }
        }
    }
}
