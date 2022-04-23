using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Historys
{
    public class GetUpdateToHistoryRequest : CreateUserRequest
    {
        public IsOperation? isOperation { get; set; }
        public string historyOperation { get; set; }
    }
}
