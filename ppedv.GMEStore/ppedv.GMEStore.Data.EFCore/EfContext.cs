using Microsoft.EntityFrameworkCore;
using ppedv.GMEStore.Model;
using System;
using System.Linq;

namespace ppedv.GMEStore.Data.EFCore
{
    public class EfContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GMEStore_dev;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                        .HasOne(x => x.Developer)
                        .WithMany(x => x.Developed)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Game>()
                        .HasOne(x => x.Publisher)
                        .WithMany(x => x.Published)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>().HasIndex(x => x.Published);


            //modelBuilder.Entity<Entity>().Property(x => x.Modified).HasValueGenerator("GetDate()").ValueGeneratedOnUpdate();

            modelBuilder.Entity<Game>().Property(x => x.Modified).IsConcurrencyToken();

            
        }


        public override int SaveChanges()
        {
            var dt = DateTime.Now;

            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                if (item.Entity is Entity en)
                {
                    en.Created = dt;
                    en.Modified = dt;
                    en.ModifiedBy = Environment.UserName;
                }
            }

            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                if (item.Entity is Entity en)
                {
                    en.Modified = dt;
                    en.ModifiedBy = Environment.UserName;
                }
            }


            return base.SaveChanges();
        }
    }
}
