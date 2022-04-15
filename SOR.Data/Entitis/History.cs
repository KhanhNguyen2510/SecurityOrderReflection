using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("Historys")]
    public class History : EntitisBase
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string HistoryOperation { get; set; }
        [Column(TypeName ="datetime")]
        public DateTime TimeOperation { get; set; }
        public IsOperation MyProperty { get; set; }
    }
}
