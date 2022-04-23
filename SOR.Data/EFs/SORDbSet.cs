using Microsoft.EntityFrameworkCore;
using SOR.Data.Entitis;

namespace SOR.Data.EFs
{
    public partial class SORDbContext : DbContext 
    {
        public DbSet<Agencies> Agencies { get; set; }
        public DbSet<State> Evaluates { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<NewsLabel> NewsLabels { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportProof> ReportProofs { get; set; }
        public DbSet<ReportResult> ReportResults { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Code> Codes { get; set; }
    }
}
