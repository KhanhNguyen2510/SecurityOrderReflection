using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.NewsLabels;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.NewLables;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/admin-panel/news-lables")]
    public class NewsLablesController : Controller
    {
        private readonly INewsLabelSevice _newsLabelSevice;
        private readonly ILogger<NewsLablesController> _logger;

        public NewsLablesController(ILogger<NewsLablesController> logger, INewsLabelSevice newsLabelSevice)
        {
            _logger = logger;
            _newsLabelSevice = newsLabelSevice;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Thêm thông tin nhãn của nhãn bài đăng")]
        public async Task<JsonResult> Create([FromForm] GetCreateToNewsLableRequest request)
        {
            try
            {
                var data = await _newsLabelSevice.CreateToNewsLable(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{Id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin nhãn của nhãn bài đăng")]
        public async Task<JsonResult> Update(string Id, [FromForm] GetUpdateToNewsLableRequest request)
        {
            try
            {
                var data = await _newsLabelSevice.UpdateToNewsLable(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Xóa thông tin nhãn của nhãn bài đăng")]
        public async Task<JsonResult> Delete(string Id, [FromForm] CreateUserRequest request)
        {
            try
            {
                var data = await _newsLabelSevice.DeleteToNewsLable(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("list-news-lables")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của nhãn bài đăng")]
        public async Task<JsonResult> List([FromQuery] GetMangagerToNewsLableRequest request)
        {
            try
            {
                var data = await _newsLabelSevice.GetListToNewsLable(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của nhãn bài đăng có phân trang")]
        public async Task<JsonResult> ListPaging([FromQuery] GetMangagerNewsLableRequest request)
        {
            try
            {
                var data = await _newsLabelSevice.GetListPagingToNewsLable(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List Paging To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List Paging To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("get-id/{Id}")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của nhãn bài đăng theo Id")]
        public async Task<JsonResult> GetById(string Id)
        {
            try
            {
                var data = await _newsLabelSevice.GetNewsLableById(Id);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get Id To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Id To NewsLable: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

    }
}
