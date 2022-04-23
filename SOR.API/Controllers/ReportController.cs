using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOR.Application.Catalogs.Reports;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Proof;
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

        public ReportController(ILogger<ReportController> logger, IReportSevice reportSevice)
        {
            _logger = logger;
            _reportSevice = reportSevice;
        }

        [HttpPost]
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Report: Message:{ex.Message}");
                throw new ApiException();
            }
        }
        [HttpPost("proofs")]
        [SwaggerOperation(Summary = "Thêm thông tin bằng chứng báo cáo")]
        public async Task<JsonResult> CreateProof([FromForm] GetCreateToReportProofRequest request)
        {
            try
            {
                var data = await _reportSevice.CreateToReportProof(request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Create To Report Proof: Message:{ex.Message}");
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Report Proof: Message:{ex.Message}");
                throw new ApiException();
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create To Report Result: Message:{ex.Message}");
                throw new ApiException();
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To Report: Message:{ex.Message}");
                throw new ApiException();
            }
        }
        [HttpPatch("{Id}/results")]
        [SwaggerOperation(Summary = "Cập nhật thônf tin kết quả báo cáo")]
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update To Report Result: Message:{ex.Message}");
                throw new ApiException();
            }
        }


        [HttpDelete]
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Report: Message:{ex.Message}");
                throw new ApiException();
            }
        }

        [HttpDelete("{Id}/proofs")]
        [SwaggerOperation(Summary = "Thêm thông tin bằng chứng báo cáo")]
        public async Task<JsonResult> DeleteProof(int Id, CreateUserRequest request)
        {
            try
            {
                var data = await _reportSevice.DeleteToReportProof(Id,request);
                return Json(data);
            }
            catch (ApiException ex)
            {
                _logger.LogError($"Delete To Report Proof: Message:{ex.Message}");
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Report Proof: Message:{ex.Message}");
                throw new ApiException();
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete To Report Result: Message:{ex.Message}");
                throw new ApiException();
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get By Id To Report : Message:{ex.Message}");
                throw new ApiException();
            }
        }

        [HttpGet("{Id}/list-reposts")]
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get list To Report : Message:{ex.Message}");
                throw new ApiException();
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
                throw new ApiException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get paging To Report : Message:{ex.Message}");
                throw new ApiException();
            }
        }
    }
}
