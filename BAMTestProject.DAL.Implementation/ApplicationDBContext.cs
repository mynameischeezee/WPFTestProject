using System.Data.Entity;
using BAMTestProject.DAL.Implementation.Entities;

namespace BAMTestProject.DAL.Implementation
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShowEntity> Shows { get; set; }
        public DbSet<MarketEntity> Markets { get; set; }
        public DbSet<BroadcastEntity> Broadcasts { get; set; }

        public ApplicationDbContext() : base("BAMDatabaseConnectionString")
        {

        }
    }
}
