using SOR.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace SOR.ViewModel.Catalogs.Historys
{
    public class GetCreateToHistoryRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập lịch sữ")]
        public string HistoryOperation { get; set; }
        [Required(ErrorMessage = "Vui chọn trang thái hoạt động")]
        public IsOperation? Operation { get; set; }
    }
}
