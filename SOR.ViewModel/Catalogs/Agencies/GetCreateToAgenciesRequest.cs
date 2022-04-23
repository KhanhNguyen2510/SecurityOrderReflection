using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Agencies
{
    public class GetCreateToAgenciesRequest : CreateUserRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ trụ sở")]
        public string office { get; set; }
        [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string numberPhone { get; set; }
    }
}
