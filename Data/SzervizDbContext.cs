using AutoszervizWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoszervizWebApp.Data
{
    public class SzervizDbContext : DbContext
    {
        public SzervizDbContext(DbContextOptions<SzervizDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Ugyfel> Ugyfelek { get; set; }
        public DbSet<Models.Javitas> Javitasok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between Konyv and Szerzo
            modelBuilder.Entity<Javitas>()
                .HasOne(k => k.Ugyfel)
                .WithMany(s => s.Javitasok)
                .HasForeignKey(k => k.UgyfelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
