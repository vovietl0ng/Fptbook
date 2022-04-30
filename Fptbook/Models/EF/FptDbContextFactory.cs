using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fptbook.Models.EF
{
    public class FptDbContextFactory : IDesignTimeDbContextFactory<FptDbContext>
    {
        public FptDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetConnectionString("FptDbContext");

            var optionsBuilder = new DbContextOptionsBuilder<FptDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new FptDbContext(optionsBuilder.Options);
        }
    }
}
