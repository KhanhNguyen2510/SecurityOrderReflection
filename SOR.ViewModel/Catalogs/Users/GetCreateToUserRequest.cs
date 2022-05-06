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
        public string identification { get; set; }
        [Display(Name = "Số điện thoại")]
        [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string numberPhone { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập mật khẩu")]
        public string password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu không khớp")]
        public string confirmPassword { get; set; }
        public string iPCreate { get; set; }
        public string agenciesId { get; set; }
    }
}
