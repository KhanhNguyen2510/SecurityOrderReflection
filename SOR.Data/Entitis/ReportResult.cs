using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// kết quả trà lời 
    /// </summary>
    [Table("ReportResult")]
    public class ReportResult : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(400)]
        public string Content { get; set; }
        [StringLength(50)]
        public string ReportId { get; set; }

        //public User User { get; set; }
    }
}
