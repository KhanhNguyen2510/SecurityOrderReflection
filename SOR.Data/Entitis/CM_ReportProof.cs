using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Báo cáo kết quả bằng chứng
    /// </summary>
    [Table("CM_ReportProof")]
    public class CM_ReportProof : EntitisBase
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
        public CM_Report Report { get; set; }
    }
}
