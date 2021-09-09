using Microsoft.EntityFrameworkCore;
using ppedv.GMEStore.Model;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public EfContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GMEStore_dev;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                        .HasOne(x => x.Developer)
                        .WithMany(x => x.Developed);

            modelBuilder.Entity<Game>()
                        .HasOne(x => x.Publisher)
                        .WithMany(x => x.Published);
        }
    }
}
