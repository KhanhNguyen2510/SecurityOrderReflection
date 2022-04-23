using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Agencies
{
    public class GetUpdateToAgenciesRequest : CreateUserRequest
    {
        public string name { get; set; }
        public string office { get; set; }
        [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string numberPhone { get; set; }
    }
}
