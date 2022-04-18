using SOR.Data.SystemBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Tài khoản đăng nhập
    /// </summary>
    [Table("Users")]
    public class User : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// số cmnd
        /// </summary>
        public string Identification { get; set; }
        public string IPCreate { get; set; }
        public IsAdmin IsAdmin { get; set; }
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
        /// <summary>
        /// Cơ quan
        /// </summary>
        public int? AgenciesId { get; set; }
                                                                                                           
        public IEnumerable<Evaluate> Evaluates { get; set; }
        public IEnumerable<History> Histories  { get; set; }
        public IEnumerable<NewsLabel> NewsLabels{ get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public IEnumerable<ReportProof> ReportProofs { get; set; }
        public IEnumerable<ReportResult> ReportResults { get; set; }
        public IEnumerable<UserPrivilege> UserPrivileges { get; set; }
    }
}
