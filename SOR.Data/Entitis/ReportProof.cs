using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Báo cáo kết quả bằng chứng
    /// </summary>
    [Table("ReportProof")]
    public class ReportProof : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Bằng chứng
        /// </summary>
        public string Proof { get; set; }
        /// <summary>
        /// Mả bài phản ánh
        /// </summary>
        /// 
        [StringLength(50)]
        public string ReportId { get; set; }
        /// <summary>
        /// Loại file truyền vào
        /// </summary>
        public IsFile IsFile  { get; set; }

        //public User User { get; set; }
        public Report Report { get; set; }
    }
}
