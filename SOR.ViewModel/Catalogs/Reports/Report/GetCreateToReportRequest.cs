using Microsoft.AspNetCore.Http;
using SOR.ViewModel.Catalogs.Reports.Proof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Reports
{
    public class GetCreateToReportRequest : CreateUserRequest
    {
        /// <summary>
        /// Nội dung báo cáo
        /// </summary>
        /// 
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }
        /// <summary>
        /// nhãn thông tin bài báo cáo 
        /// </summary>
        /// 
        [Required(ErrorMessage = "Vui lòng nhập Id của nhãn")]
        public string newsLabelId { get; set; }
        /// <summary>
        /// Ip mạng của user đó
        /// </summary>
        /// 
        public string iP { get; set; }
        /// <summary>
        /// Máy sử dụng để report 
        /// </summary>
        /// 
        public string userAngel { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ báo cáo")]
        public string locationReport { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        public string locationUser { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chọn hình ảnh")]
        public List<IFormFile> files { get; set; }
    }
}
