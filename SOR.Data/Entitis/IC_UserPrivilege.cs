using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Quyền của user
    /// </summary>
    [Table("IC_UserPrivilege")]
    public class IC_UserPrivilege : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// dành cho user mặc định ///    
        /// true: dành cho user
        /// fales : không dành cho user
        /// </summary>
        public bool UseForDefault { get; set; } = true;
        /// <summary>
        /// Xem có phải là admin không
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        [StringLength(2000)]
        public string Note { get; set; }
    }
}
