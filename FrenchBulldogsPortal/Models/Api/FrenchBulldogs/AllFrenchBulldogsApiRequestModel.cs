namespace FrenchBulldogsPortal.Models.Api.FrenchBulldogs
{
    public class AllFrenchBulldogsApiRequestModel
    {
        public string Name { get; init; }

        public string SearchTerm { get; init; }

        public FrenchBulldogSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int CarsPerPage { get; init; } = 10;
    }
}
