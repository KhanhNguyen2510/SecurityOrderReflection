using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("IC_PrivilegeDetail")]
    public class IC_PrivilegeDetails : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PrivilegeIndex { get; set; }
        /// <summary>
        /// Thuộc From nào
        /// </summary>
        [StringLength(100)]
        public string FormName { get; set; }
        /// <summary>
        /// Quyền sử dụng là quyền gì 
        /// </summary>
        public IsRole IsRole { get; set; }
    }
}
