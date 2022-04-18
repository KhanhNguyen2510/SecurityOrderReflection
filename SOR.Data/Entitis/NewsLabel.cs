using SOR.Data.SystemBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Nhản của bài đăng
    /// </summary>
    [Table("NewsLabel")]
    public  class NewsLabel : EntitisBase
    {
        [Key]
        [Column(TypeName = "nvarchar(6)")]
        public string Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
