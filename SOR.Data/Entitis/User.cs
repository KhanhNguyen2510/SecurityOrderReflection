using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Tài khoản đăng nhập
    /// </summary>
    [Table("User")]
    public class User : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string PassWord { get; set; }
        public IsGender? Gender { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [StringLength(15)]
        public string NumberPhone { get; set; }
        /// <summary>
        /// số cmnd
        /// </summary>
        /// 
        [StringLength(14)]
        public string Identification { get; set; }
        [StringLength(200)]
        public string IPCreate { get; set; }
        public IsAdmin IsAdmin { get; set; }
        /// <summary>
        /// Login vào
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? FistLogin { get; set; }
        /// <summary>
        /// Logout ra
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndLogin { get; set; }
        /// <summary>
        /// Cơ quan
        /// </summary>
        /// 
        [StringLength(200)]
        public int? AgenciesId { get; set; }

        //public IEnumerable<State> State { get; set; }
        //public IEnumerable<History> Histories  { get; set; }
        //public IEnumerable<NewsLabel> NewsLabels{ get; set; }
        //public IEnumerable<Report> Reports { get; set; }
        //public IEnumerable<ReportProof> ReportProofs { get; set; }
        //public IEnumerable<ReportResult> ReportResults { get; set; }
    }
}
