using EfMigrations.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfMigrations.Data
{
    public class EfContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }

        public EfContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Games;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(x => x.Description)
                                       .HasMaxLength(400)
                                       .HasColumnName("Beschreibung")
                                       .IsRequired();

            modelBuilder.Entity<Game>().Ignore(x => x.NichtInDB);

            //shadow property
            modelBuilder.Entity<Game>().Property<DateTime>("LastEdit");



            modelBuilder.Entity<Game>()
                        .HasOne<Company>(x => x.Developer)
                        .WithMany(x => x.Developed)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

            modelBuilder.Entity<Game>().HasOne<Company>(x => x.Publisher).WithMany(x => x.Published);

            
            modelBuilder.ApplyConfiguration(new DlcModelConfig());
        }
    }
}
