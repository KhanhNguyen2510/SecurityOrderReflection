using SOR.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetCreateToUserRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập tên tài khoản đăng nhập")]
        public string userName { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]*", ErrorMessage = "Email không đúng định dạng")]
        public string email { get; set; }
        public IsGender? gender { get; set; } = IsGender.Orther;
        public string fullName { get; set; }
        [Display(Name = "Số điện thoại")]
        public string numberPhone { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập mật khẩu")]
        public string password { get; set; }
        public string iPCreate { get; set; }
    }
}
