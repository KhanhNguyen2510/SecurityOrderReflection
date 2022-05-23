using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Nhản của bài đăng
    /// </summary>
    [Table("CM_NewsLabel")]
    public  class CM_NewsLabel : EntitisBase
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(500)]
        public string Name { get; set; }

        //public User User { get; set; }
        //public IEnumerable<Report> Reports { get; set; }
    }
}
