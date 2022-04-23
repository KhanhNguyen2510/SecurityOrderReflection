using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Reports.Proof
{
    public class GetCreateToReportProofRequest : CreateUserRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập chọn hình ảnh")]
        public string proof { get; set; }
        /// <summary>
        /// Mả bài phản ánh
        /// </summary>
        /// 
        [Required(ErrorMessage = "Vui lòng nhập Id báo cáo")]
        public string reportId { get; set; }
    }
}
