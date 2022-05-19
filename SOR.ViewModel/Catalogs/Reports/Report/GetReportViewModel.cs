using SOR.Data.Enum;
using System;
using System.Collections.Generic;

namespace SOR.ViewModel.Catalogs.Reports.Report
{

    public class GetReportViewModel
    {
        public string Title { get; set; }
        public IsReport IsReport { get; set; }
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
        public List<Results> rResults { get; set; }

        public List<Proofs> rProofs { get; set; }
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
    public class Results
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }

    public class Proofs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IsFile IsFile { get; set; }
    }
}
