using Microsoft.EntityFrameworkCore;
using SOR.Data.Entitis;

namespace SOR.Data.EFs
{
    public partial class SORDbContext : DbContext
    {
        public DbSet<TA_Code> Codes { get; set; }
        public DbSet<TA_Agencies> Agencies { get; set; }
        public DbSet<TA_State> Evaluates { get; set; }

        public DbSet<CM_History> Histories { get; set; }
        public DbSet<CM_NewsLabel> NewsLabels { get; set; }
        public DbSet<CM_Report> Reports { get; set; }
        public DbSet<CM_ReportProof> ReportProofs { get; set; }
        public DbSet<CM_ReportResult> ReportResults { get; set; }


        public DbSet<IC_User> Users { get; set; }
        public DbSet<IC_PrivilegeDetails> PrivilegeDetails { get; set; }
        public DbSet<IC_UserPrivilege> UserPrivileges { get; set; }
    }
}
