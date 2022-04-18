using SOR.Data.SystemBase;
using System;
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
        public string Content { get; set; }
        /// <summary>
        /// nhãn thông tin bài báo cáo 
        /// </summary>
        public string NewsLabelId { get; set; }
        /// <summary>
        /// Ip mạng của user đó
        /// </summary>
        public string IPUser { get; set; }
        /// <summary>
        /// Máy sử dụng để report 
        /// </summary>
        public string UserAngel { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        public string LocationReport { get; set; }
        /// <summary>
        /// địa chỉ báo cáo
        /// </summary>
        public string LocationUser { get; set; }
        /// <summary>
        /// số lượng người xem tin này
        /// </summary>
        public int Views { get; set; }
        /// <summary>
        /// Ngày giải quyết
        /// </summary>
        [Column(TypeName ="datetime")]
        public DateTime DateSolve
        {
            get { return DateSolve; }
            set { DateSolve = DateTime.Now.AddDays(10);}
        }

        public User User { get; set; }
        public NewsLabel NewsLabel { get; set; }
    }
}
