using ConsertsModel;
using Microsoft.EntityFrameworkCore;

namespace DBContext
{
    public class ConcertsDB : DbContext
    {
        public ConcertsDB(DbContextOptions<ConcertsDB> options) : base(options) { }

        public DbSet<Concert> Concerts => Set<Concert>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use lowercase table names
            modelBuilder.Entity<Concert>().ToTable("concerts");
        }
    }

}