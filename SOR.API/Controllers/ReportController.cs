using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Reports;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Reports.Result;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;


namespace SOR.WedAPI.Controllers
{
    [ApiController]
    [Route("V1/user-panel/reports")]
    public class ReportController : Controller
    {
        private readonly IReportSevice _reportSevice;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, IReportSevice reportSevice )
        {
            _logger = logger;
            _reportSevice = reportSevice;
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = /*268435456*/ 737280000)]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(737280000)]
        [SwaggerOperation(Summary = "Thêm thông tin bài báo cáo")]
        public async Task<JsonResult> Create([FromForm] GetCreateToReportRequest request)
        {
            try
            {
                var data = await _reportSevice.CreateToReport(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        
        [HttpPost("results")]
        [SwaggerOperation(Summary = "Thêm thông tin bằng chứng báo cáo")]
        public async Task<JsonResult> CreateResult([FromForm] GetCreateToReportResultRequest request)
        {
            try
            {
                var data = await _reportSevice.CreateToReportResult(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{Id}/report-is-status")]
        [SwaggerOperation(Summary = "Cập nhật trạng thái thông tin bài báo cáo")]
        public async Task<JsonResult> UpdateStatus(string Id, [FromForm] GetUpdateStatusInReportRequest request)
        {
            try
            {
                var data = await _reportSevice.UpdateStatus(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update IsStatus To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update IsStatus To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{Id}/report-is-report")]
        [SwaggerOperation(Summary = "Cập nhật  thông tin báo cáo của bài báo cáo")]
        public async Task<JsonResult> UpdateIsReport(string Id, [FromForm] GetUpdateReportInReportRequest request)
        {
            try
            {
                var data = await _reportSevice.UpdateIsReport(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update IsReport To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update IsReport To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpPatch("{Id}")]
        [SwaggerOperation(Summary = "Cập nhật thông tin bài báo cáo")]
        public async Task<JsonResult> Update(string Id,[FromForm] GetUpdateToReportRequest request)
        {
            try
            {
                var data = await _reportSevice.UpdateToReport(Id,request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }
        [HttpPatch("{Id}/results")]
        [SwaggerOperation(Summary = "Cập nhật thông tin kết quả báo cáo")]
        public async Task<JsonResult> UpdateProof(int Id,[FromForm] GetUpdateToReportResultRequest request)
        {
            try
            {
                var data = await _reportSevice.UpdateToReportResult(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Update To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation(Summary = "Xóa thông tin bài báo cáo")]
        public async Task<JsonResult> Delete(string Id, CreateUserRequest request)
        {
            try
            {
                var data = await _reportSevice.DeleteToReport(Id, request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Report: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpDelete("{Id}/results")]
        [SwaggerOperation(Summary = "Thêm thông tin bằng chứng báo cáo")]
        public async Task<JsonResult> DeleteResult(int Id, CreateUserRequest request)
        {
            try
            {
                var data = await _reportSevice.DeleteToReportResult(Id,request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Report Result: Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Hiển thị thông tin báo cáo theo Id")]
        public async Task<JsonResult> GetById(string Id)
        {
            try
            {
                var data = await _reportSevice.GetReportById(Id);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get By Id To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get By Id To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet("list-reposts")]
        [SwaggerOperation(Summary = "Hiển thị tất cả thông tin báo cáo ")]
        public async Task<JsonResult> GetList([FromQuery]GetMangagerToReportRequest request)
        {
            try
            {
                var data = await _reportSevice.GetListToReport(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get list To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get list To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hiển thị có phân trang thông tin báo cáo")]
        public async Task<JsonResult> GetPaging([FromQuery]GetMangagerReportRequest request)
        {
            try
            {
                var data = await _reportSevice.GetListPagingToReport(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Get paging To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get paging To Report : Message:{ex.Message}");
               throw new ApiException(ex.Message);
            }
        }
    }
}
