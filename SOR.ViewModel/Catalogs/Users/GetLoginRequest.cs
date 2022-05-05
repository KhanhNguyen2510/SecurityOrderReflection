using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetLoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên tài khoản")]
        public string userName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần phải nhập mật khẩu")]
        public string passWord { get; set; }
    }
}
