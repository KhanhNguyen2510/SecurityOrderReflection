using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Historys
{
    public class GetUpdateToHistoryRequest
    {
        public IsOperation? Operation { get; set; }
        public string HistoryOperation { get; set; }
    }
}
