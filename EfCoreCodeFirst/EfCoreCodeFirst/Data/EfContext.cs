using EfCoreCodeFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCodeFirst.Data
{
    public class EfContext : DbContext
    {
        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        //public DbSet<Person> People { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        public EfContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table-per-Type
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Kunde>().ToTable("Kunden");
            modelBuilder.Entity<Mitarbeiter>().ToTable("Mitarbeiter");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocaldb;Database=EfCodeFirst;Trusted_Connection=true");
        }

    }
}
