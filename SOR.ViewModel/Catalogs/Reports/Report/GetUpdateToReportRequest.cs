namespace SOR.ViewModel.Catalogs.Reports
{
    public class GetUpdateToReportRequest : CreateUserRequest
    {
        public string newsLabelId { get; set; }
        public string content { get; set; }
    }
}
