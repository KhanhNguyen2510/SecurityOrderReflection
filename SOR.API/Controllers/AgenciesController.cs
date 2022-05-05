using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Agencies;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Agencies;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;



namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/admin-panel/agencies")]
    public class AgenciesController : Controller
    {
        private readonly IAgenciesSevice _agenciesSevice;
        private readonly ILogger<AgenciesController> _logger;

        public AgenciesController(ILogger<AgenciesController> logger, IAgenciesSevice agenciesSevice)
        {
            _logger = logger;
            _agenciesSevice = agenciesSevice;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Thêm thông tin nhãn của cơ quan")]
        public async Task<JsonResult> Create([FromForm] GetCreateToAgenciesRequest request)
        {
            try
            {
                var data = await _agenciesSevice.CreateToAgencies(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{Id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin nhãn của cơ quan")]
        public async Task<JsonResult> Update(string Id, [FromForm] GetUpdateToAgenciesRequest request)
        {
            try
            {
                var data = await _agenciesSevice.UpdateToAgencies(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Xóa thông tin nhãn của cơ quan")]
        public async Task<JsonResult> Delete(string Id, [FromForm]CreateUserRequest request)
        {
            try
            {
                var data = await _agenciesSevice.DeleteToAgencies(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("list-agencies")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của cơ quan")]
        public async Task<JsonResult> List([FromQuery] GetMangagerToAgenciesRequest request)
        {
            try
            {
                var data = await _agenciesSevice.GetListToAgencies(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của cơ quan có phân trang")]
        public async Task<JsonResult> ListPaging([FromQuery] GetMangagerAgenciesRequest request)
        {
            try
            {
                var data = await _agenciesSevice.GetListPagingToAgencies(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"List Paging To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"List Paging To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("{Id}/get-id")]
        [SwaggerOperation(Summary = "Hiển thị thông tin nhãn của cơ quan theo Id")]
        public async Task<JsonResult> GetById(string Id)
        {
            try
            {
                var data = await _agenciesSevice.GetAgenciesById(Id);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get Id To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Id To Agencies: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }
    }
}
