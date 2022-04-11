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

        public DbSet<Category> Categories { get; init; }

        public DbSet<Dealer> Dealers { get; init; }

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
                .HasOne(c => c.Dealer)
                .WithMany(d => d.FrenchBulldogs)
                .HasForeignKey(c => c.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
