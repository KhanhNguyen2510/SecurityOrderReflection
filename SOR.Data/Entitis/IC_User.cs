﻿using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOR.Data.Entitis
{
    /// <summary>
    /// Tài khoản đăng nhập
    /// </summary>
    [Table("IC_User")]
    public class IC_User : EntitisBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(500)]
        public string UserName { get; set; }
        [StringLength(500)]
        public string Email { get; set; }
        [StringLength(50)]
        public string PassWord { get; set; }
        public IsGender? Gender { get; set; }
        [StringLength(500)]
        public string FullName { get; set; }
        [StringLength(15)]
        public string NumberPhone { get; set; }
        /// <summary>
        /// số cmnd
        /// </summary>
        /// 
        [StringLength(100)]
        public string Identification { get; set; }
        [StringLength(200)]
        public string IPCreate { get; set; }
        public IsAdmin IsAdmin { get; set; } = IsAdmin.People;
        /// <summary>
        /// Login vào
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? FistLogin { get; set; } = DateTime.Now;
        /// <summary>
        /// Logout ra
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndLogin { get; set; }
        /// <summary>
        /// Cơ quan
        /// </summary>
        /// 
        public string AgenciesId { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// Đặc quyền tài khoản
        /// </summary>
        public short AccountPrivilege { get; set; }




        //public IEnumerable<State> State { get; set; }
        //public IEnumerable<History> Histories  { get; set; }
        //public IEnumerable<NewsLabel> NewsLabels{ get; set; }
        //public IEnumerable<Report> Reports { get; set; }
        //public IEnumerable<ReportProof> ReportProofs { get; set; }
        //public IEnumerable<ReportResult> ReportResults { get; set; }
    }
}
