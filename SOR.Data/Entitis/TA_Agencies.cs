using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Cơ quan
    /// </summary>
    [Table("TA_Agencies")]
    public class TA_Agencies : EntitisBase
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Trụ sở
        /// </summary>
        /// 
        public string Office { get; set; }
        [StringLength(15)]
        public string NumberPhone { get; set; }

        //public User User { get; set; }
    }
}
