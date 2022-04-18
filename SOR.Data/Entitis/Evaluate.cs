using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("Evaluate")]
    public class Evaluate : EntitisBase
    {
        [Key]
        public int Id { get; set; }
        public IsEvaluate IsEvaluate { get; set; }

        public User User { get; set; }
    }
}
