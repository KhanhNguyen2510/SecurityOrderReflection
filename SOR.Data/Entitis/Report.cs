using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("Report")]
    public class Report : EntitisBase
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Nội dung báo cáo
        /// </summary>
        /// 
        [StringLength(500)]
        public string Content { get; set; }
        /// <summary>
        /// nhãn thông tin bài báo cáo 
        /// </summary>
        /// 
        [StringLength(10)]
        public string NewsLabelId { get; set; }
        /// <summary>
        /// Ip mạng của user đó
        /// </summary>
        /// 
        [StringLength(50)]
        public string IP { get; set; }
        /// <summary>
        /// Máy sử dụng để report 
        /// </summary>
        /// 
        [StringLength(200)]
        public string UserAngel { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        [StringLength(300)]
        public string LocationReport { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        /// 
        [StringLength(300)]
        public string LocationUser { get; set; }
        /// <summary>
        /// số lượng người xem tin này
        /// </summary>
        public int? Views { get; set; }
        /// <summary>
        /// Ngày giải quyết
        /// </summary>
        [Column(TypeName ="datetime")]
        public DateTime DateSolve
        {
            get { return DateSolve; }
            set { DateSolve = DateTime.Now.AddDays(10);}
        }
        public IsStatus? IsStatus { get; set; }


        public User User { get; set; }
        public NewsLabel NewsLabel { get; set; }
        public IEnumerable<ReportProof>  ReportProofs { get; set; }
    }
}
