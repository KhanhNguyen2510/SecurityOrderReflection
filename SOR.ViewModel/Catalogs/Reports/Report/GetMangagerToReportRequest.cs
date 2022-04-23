using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Reports.Report
{
    public class GetMangagerToReportRequest
    {
        public string keyWord { get; set; }
        public IsStatus? IsStatus { get; set; }
        public string NewslableId { get; set; }
    }
}
