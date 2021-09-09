using EfMigrations.Model;
using Microsoft.EntityFrameworkCore;

namespace EfMigrations.Data
{
    public class EfContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public EfContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Games;Trusted_Connection=true");
        }
    }
}
