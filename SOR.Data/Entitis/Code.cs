using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Key của từng khu vực
    /// </summary>
    [Table("Code")]
    public class Code : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(300)]
        public string Name { get; set; }
        [StringLength(4)]
        public string Key { get; set; }
    }
}
