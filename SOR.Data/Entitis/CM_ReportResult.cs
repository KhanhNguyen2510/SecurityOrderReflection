using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// kết quả trà lời 
    /// </summary>
    [Table("CM_ReportResult")]
    public class CM_ReportResult : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        [StringLength(50)]
        public string ReportId { get; set; }


        //public User User { get; set; }
    }
}
