using SOR.Data.Enum;
using System;

namespace SOR.ViewModel.Catalogs.Reports.Report
{
    public class GetMangagerToReportRequest : CreateUserRequest
    {
        public string keyWord { get; set; }
        public IsStatus? isStatus { get; set; }
        public string newslableId { get; set; }
        public IsDate? isDate { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
    }
}
