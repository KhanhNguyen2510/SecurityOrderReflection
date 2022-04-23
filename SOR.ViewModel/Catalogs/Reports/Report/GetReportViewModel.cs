using SOR.Data.Enum;
using System;

namespace SOR.ViewModel.Catalogs.Reports
{
    public class GetReportViewModel
    {
        public string Id { get; set; }
        /// <summary>
        /// Nội dung báo cáo
        /// </summary>
        /// 
        public string Content { get; set; }
        /// <summary>
        /// nhãn thông tin bài báo cáo 
        /// </summary>
        /// 
        public string Proof { get; set; }
        public int ProofId { get; set; }

        public string Result { get; set; }
        public int ResultId { get; set; }

        public string NewsLabelId { get; set; }
        public string NewsLabelName { get; set; }
        /// <summary>
        /// Ip mạng của user đó
        /// </summary>
        /// 
        public string IP { get; set; }
        /// <summary>
        /// Máy sử dụng để report 
        /// </summary>
        /// 
        public string UserAngel { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        public string LocationReport { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        public string LocationUser { get; set; }
        /// <summary>
        /// số lượng người xem tin này
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// Ngày giải quyết
        /// </summary>
        public DateTime DateSolve { get; set; }
        public IsStatus IsStatus { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
