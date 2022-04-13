namespace FrenchBulldogsPortal.Services.FrenchBulldogs.Models
{
    public class LatestFrenchBulldogServiceModel : IFrenchBulldogModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ColorName { get; init; }

        public string ImageUrl { get; init; }

        public int Age { get; init; }
    }
}
