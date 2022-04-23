using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.NewLables
{
    public class GetCreateToNewsLableRequest : CreateUserRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập Id")]
        public string id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string name { get; set; }
    }
}
