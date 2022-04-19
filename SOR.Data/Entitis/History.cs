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
    [Table("Historys")]
    public class History : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(200)]
        public string HistoryOperation { get; set; }
        [Column(TypeName ="datetime")]
        public DateTime? TimeOperation { get; set; } = DateTime.Now;
        /// <summary>
        /// Trạng thái hoạt động
        /// </summary>
        public IsOperation? Operation { get; set; }

        public User User { get; set; }
    }
}
