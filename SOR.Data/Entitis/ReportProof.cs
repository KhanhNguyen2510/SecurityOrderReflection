using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("ReportImage")]
    public class ReportProof : EntitisBase
    {
        [Key]
        public int Id { get; set; }
        public string Proof { get; set; }
        public string ReportId { get; set; }
        public User User { get; set; }
    }
}
