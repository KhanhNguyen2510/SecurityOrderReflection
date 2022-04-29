using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Reports.Report
{
    public class GetUpdateStatusInReportRequest : CreateUserRequest
    {
        public IsStatus? IsStatus { get; set; }
    }
}
