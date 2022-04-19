using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Cơ quan
    /// </summary>
    [Table("Agencies")]
    public class Agencies : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Trụ sở
        /// </summary>
        /// 
        [StringLength(200)]
        public string Office { get; set; }
        [StringLength(15)]
        public string NumberPhone { get; set; }

        public User User { get; set; }
    }
}
