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
        [StringLength(400)]
        public string Proof { get; set; }
        public string ReportId { get; set; }
       
        public User User { get; set; }
        public Report Report { get; set; }
    }
}
