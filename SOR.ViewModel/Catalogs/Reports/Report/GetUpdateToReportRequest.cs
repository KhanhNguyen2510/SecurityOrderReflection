using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Reports.Report
{
    public class GetUpdateToReportRequest : CreateUserRequest
    {
        public string newsLabelId { get; set; }
        public string content { get; set; }
    }
}
