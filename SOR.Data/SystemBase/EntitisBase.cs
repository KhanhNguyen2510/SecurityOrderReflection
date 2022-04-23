using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.SystemBase
{
    public class EntitisBase
    {
        public bool IsDelete { get; set; } = true;
        [Column(TypeName = "datetime")]
        public DateTime? TimeDelete { get; set; }
        

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string CreateUser { get; set; }
        [StringLength(100)]
        public string UpdateUser { get; set; }
    }
}
