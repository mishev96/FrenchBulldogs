namespace FrenchBulldogsPortal.Services.FrenchBulldogs.Models
{
    public class FrenchBulldogServiceModel : IFrenchBulldogModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Color { get; init; }

        public string ImageUrl { get; init; }

        public int Age { get; init; }

        public string CategoryName { get; init; }
    }
}
