using SOR.Data.Enum;
using SOR.ViewModel.Common;

namespace SOR.ViewModel.Catalogs.Reports.Report
{
    public class GetMangagerReportRequest : PagingRequestBase
    {
        public string keyWord { get; set; }
        public IsStatus? IsStatus { get; set; }
        public string NewslableId { get; set; }
    }
}
