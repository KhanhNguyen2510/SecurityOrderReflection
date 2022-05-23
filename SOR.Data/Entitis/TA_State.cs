using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Trạng thái kết quả
    /// </summary>
    [Table("TA_State")]
    public class TA_State : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Vui/không vui 
        /// </summary>
        public IsState IsState { get; set; } = IsState.UnStae;
        [StringLength(50)]
        public string ReportId { get; set; }

        //public User User { get; set; }
    }
}
