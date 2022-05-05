using SOR.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetUpdateToUserRequest
    {
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]*", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Identification { get; set; }
        public string AgenciesId { get; set; }
        public IsGender? Gender { get; set; }
        [Display(Name = "Số điện thoại")]
        [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string NumberPhone { get; set; }
    }
}
