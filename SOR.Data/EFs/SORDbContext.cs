using Microsoft.EntityFrameworkCore;
using SOR.Data.Entitis;

namespace SOR.Data.EFs
{
    public class SORDbContext : DbContext
    {
        public SORDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Evaluate>().HasOne(x => x.User).WithMany(x => x.Evaluates).HasForeignKey(x => x.CreateUser);
            builder.Entity<History>().HasOne(x => x.User).WithMany(x => x.Histories).HasForeignKey(x => x.CreateUser);
            builder.Entity<NewsLabel>().HasOne(x => x.User).WithMany(x => x.NewsLabels).HasForeignKey(x => x.CreateUser);
            builder.Entity<Report>().HasOne(x => x.User).WithMany(x => x.Reports).HasForeignKey(x => x.CreateUser);
            builder.Entity<ReportProof>().HasOne(x => x.User).WithMany(x => x.ReportProofs).HasForeignKey(x => x.CreateUser);
            builder.Entity<ReportResult>().HasOne(x => x.User).WithMany(x => x.ReportResults).HasForeignKey(x => x.CreateUser);
        }

        public DbSet<Agencies> Agencies { get; set; }
        public DbSet<Evaluate> Evaluates { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<NewsLabel> NewsLabels { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportProof> ReportProofs { get; set; }
        public DbSet<ReportResult> ReportResults { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
