using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Lịch sữ thu thập
    /// </summary>
    [Table("CM_History")]
    public class CM_History : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Lịch sử thu thập nội dung
        /// </summary>
        public string HistoryOperation { get; set; }
        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        [Column(TypeName ="datetime")]
        public DateTime TimeOperation { get; set; } = DateTime.Now;
        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public IsOperation? IsOperation { get; set; } = Enum.IsOperation.Orther;

        //public User User { get; set; }
    }
}
