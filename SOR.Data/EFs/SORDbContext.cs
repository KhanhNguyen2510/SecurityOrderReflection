using Microsoft.EntityFrameworkCore;
using SOR.Data.Entitis;

namespace SOR.Data.EFs
{
    public partial class SORDbContext : DbContext
    {
        public object value;

        public SORDbContext()
        {
        }

        public SORDbContext(DbContextOptions<SORDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<History>().HasOne(x => x.User).WithMany(x => x.Histories).HasForeignKey(x => x.CreateUser);
            //builder.Entity<State>().HasOne(x => x.User).WithMany(x => x.State).HasForeignKey(x => x.CreateUser);

            //builder.Entity<NewsLabel>().HasOne(x => x.User).WithMany(x => x.NewsLabels).HasForeignKey(x => x.CreateUser);

            //builder.Entity<Report>().HasOne(x => x.User).WithMany(x => x.Reports).HasForeignKey(x => x.CreateUser);

            //builder.Entity<ReportProof>().HasOne(x => x.User).WithMany(x => x.ReportProofs).HasForeignKey(x => x.CreateUser);

            //builder.Entity<ReportResult>().HasOne(x => x.User).WithMany(x => x.ReportResults).HasForeignKey(x => x.CreateUser);

            //builder.Entity<Report>().HasOne(x => x.NewsLabel).WithMany(x => x.Reports).HasForeignKey(x => x.NewsLabelId);

            //builder.Entity<ReportProof>().HasOne(x => x.Report).WithMany(x => x.ReportProofs).HasForeignKey(x => x.ReportId);
        }

    }
}
