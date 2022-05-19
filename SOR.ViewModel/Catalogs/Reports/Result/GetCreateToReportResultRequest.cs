using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Reports.Result
{
    public class GetCreateToReportResultRequest : CreateUserRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Id báo cáo")]
        public string reportId { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
