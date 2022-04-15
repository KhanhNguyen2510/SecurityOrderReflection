using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    [Table("Users")]
    public class User : EntitisBase
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// số cmnd
        /// </summary>
        public string Identification { get; set; }
        public string IPCreate { get; set; }
        /// <summary>
        /// Login vào
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime FistLogin { get; set; }
        /// <summary>
        /// Logout ra
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime EndLogin { get; set; }
        public IsAdmin IsAdmin { get; set; }
    }
}
