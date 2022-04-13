namespace FrenchBulldogsPortal.Services.Breeders
{
    using System.Linq;
    using FrenchBulldogsPortal.Data;

    public class BreederService : IBreederService
    {
        private readonly FrenchBulldogsDbContext data;

        public BreederService(FrenchBulldogsDbContext data) 
            => this.data = data;

        public bool IsBreeder(string userId)
            => this.data
                .Breeders
                .Any(d => d.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Breeders
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
