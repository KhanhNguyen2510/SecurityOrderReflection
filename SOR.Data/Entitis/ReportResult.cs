using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("ReportResult")]
    public class ReportResult : EntitisBase
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
    }
}
