using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Historys;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/admin-panel/historys")]
    public class HistorysController : Controller
    {
        private readonly IHistorySevice _historySevice;
        private readonly ILogger<HistorysController> _logger;

        public HistorysController(ILogger<HistorysController> logger, IHistorySevice historySevice)
        {
            _logger = logger;
            _historySevice = historySevice;
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Thêm thông tin nhãn của lịch sử")]
        public async Task<JsonResult> Create([FromForm] GetCreateToHistoryRequest request)
        {
            try
            {
                var data = await _historySevice.CreateToHistory(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("list-historys")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của lịch sử")]
        public async Task<JsonResult> List([FromQuery] GetMangagerToHistoryRequest request)
        {
            try
            {
                var data = await _historySevice.GetListToHistory(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của lịch sử có phân trang")]
        public async Task<JsonResult> ListPaging([FromQuery] GetMangagerHistoryRequest request)
        {
            try
            {
                var data = await _historySevice.GetListPagingToHistory(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List Paging To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List Paging To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("{Id}/get-id")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của lịch sử theo Id")]
        public async Task<JsonResult> GetById(int Id)
        {
            try
            {
                var data = await _historySevice.GetHistoryById(Id);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get Id To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Id To History: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }
    }
}
