namespace FrenchBulldogsPortal.Services.FrenchBulldogs.Models
{
    public class FrenchBulldogDetailsServiceModel : FrenchBulldogServiceModel
    {
        public string Description { get; init; }

        public int ColorId { get; init; }

        public int CategoryId { get; init; }

        public int BreederId { get; init; }

        public string BreederName { get; init; }

        public string UserId { get; init; }
    }
}
