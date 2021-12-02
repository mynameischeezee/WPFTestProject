using System.Data.Entity;
using BAMTestProject.DAL.Implementation.Models;

namespace BAMTestProject.DAL.Implementation
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }

        public ApplicationDbContext() : base("BAMDatabaseConnectionString")
        {
            
        }
        //TODO: Add entity configurations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
