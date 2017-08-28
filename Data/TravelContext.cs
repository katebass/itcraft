using Travels.Models;
using Microsoft.EntityFrameworkCore;

namespace Travels.Data
{
    public class TravelContext: DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options) : base(options)
        {
        }

        public DbSet<Clients> Clients { get; set; }
        public DbSet<Tours> Tours { get; set; }
        public DbSet<Excursion_Sights> Excursion_Sights { get; set; }
        public DbSet<Tours_Excursions> Tours_Excursions { get; set; }
        public DbSet<Tours_Clients> Tours_Clients { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>().ToTable("Clients");
            modelBuilder.Entity<Tours>().ToTable("Tours");
            modelBuilder.Entity<Excursion_Sights>().ToTable("Excursion_Sights");
            modelBuilder.Entity<Tours_Excursions>().ToTable("Tours_Excursions");
            modelBuilder.Entity<Tours_Clients>().ToTable("Tours_Clients");
            modelBuilder.Entity<Users>().ToTable("Users");
        }
    }
}
