using FrenchBulldogs.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FrenchBulldogs.Data
{
    public class FrenchBulldogDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FrenchBulldog;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<FrenchBulldog> FrenchBulldogs { get; set; }

        public DbSet<UserFrenchBulldog> UserFrenchBulldogs { get; set; }
    }
}