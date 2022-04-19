using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.SystemBase
{

    public class EntitisBase
    {
        public bool IsDelete { get; set; } = false;
        [Column(TypeName = "datetime")]
        public DateTime? TimeDelete { get; set; }
        public int? UpdateUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; } = DateTime.Now.AddDays(10);

        public int? CreateUser { get; set; }
    }
}
