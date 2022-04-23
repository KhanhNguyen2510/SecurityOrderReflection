using SOR.Data.Enum;
using System;

namespace SOR.ViewModel.Catalogs.Historys
{
    public class GetHistoryViewModel
    {
        public int Id { get; set; }
        public string HistoryOperation { get; set; }
        public DateTime TimeOperation { get; set; }
        public IsOperation? IsOperation { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
