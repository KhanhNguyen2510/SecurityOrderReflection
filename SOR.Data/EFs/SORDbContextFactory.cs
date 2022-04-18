using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SOR.Data.EFs
{
    public class SORDbContextFactory : IDesignTimeDbContextFactory<SORDbContext>
    {
        public SORDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SORDbContext>();

            optionsBuilder.UseSqlServer(connectionString);
            return new SORDbContext(optionsBuilder.Options);
        }
    }
}
