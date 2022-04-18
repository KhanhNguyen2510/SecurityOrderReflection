using SOR.Data.SystemBase;
using System.Collections.Generic;
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
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        public User User { get; set; }
        public IEnumerable<Report> Reports { get; set; }
    }
}
