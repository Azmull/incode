
using Microsoft.EntityFrameworkCore;

namespace incode.Models
{
    public partial class incodedatabaseContext : DbContext
    {
        public incodedatabaseContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration Config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(Config.GetConnectionString("incodedatabase"));
            }
        }
    }
}

