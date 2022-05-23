using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Key của từng khu vực
    /// </summary>
    [Table("TA_Code")]
    public class TA_Code : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(1000)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Key { get; set; }
    }
}
