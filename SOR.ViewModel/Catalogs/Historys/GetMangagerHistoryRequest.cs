using SOR.Data.Enum;
using SOR.ViewModel.Common;

namespace SOR.ViewModel.Catalogs.Historys
{
    public class GetMangagerHistoryRequest : PagingRequestBase
    {
        public string keyWord { get; set; }
        public IsOperation? isOperation { get; set; }
    }
}
