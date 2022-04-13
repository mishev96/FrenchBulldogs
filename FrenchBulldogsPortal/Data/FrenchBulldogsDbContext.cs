namespace FrenchBulldogsPortal.Data
{
    using FrenchBulldogsPortal.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FrenchBulldogsDbContext : IdentityDbContext<User>
    {
        public FrenchBulldogsDbContext(DbContextOptions<FrenchBulldogsDbContext> options)
            : base(options)
        {
        }

        public DbSet<FrenchBulldog> FrenchBulldogs { get; init; }

        public DbSet<Color> Colors { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Breeder> Breeders { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<FrenchBulldog>()
                .HasOne(c => c.Category)
                .WithMany(c => c.FrenchBulldogs)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<FrenchBulldog>()
                .HasOne(c => c.Color)
                .WithMany(c => c.FrenchBulldogs)
                .HasForeignKey(c => c.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<FrenchBulldog>()
                .HasOne(c => c.Breeder)
                .WithMany(d => d.FrenchBulldogs)
                .HasForeignKey(c => c.BreederId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Breeder>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Breeder>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
