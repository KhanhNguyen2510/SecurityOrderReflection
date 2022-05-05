using SOR.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetCreateToUserRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập tên tài khoản đăng nhập")]
        public string UserName { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]*", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        public IsGender? Gender { get; set; } = IsGender.Orther;
        public string FullName { get; set; }
        public string Identification { get; set; }
        [Display(Name = "Số điện thoại")]
        [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string NumberPhone { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        public string IPCreate { get; set; }
        public string AgenciesId { get; set; }
    }
}
