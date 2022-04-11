namespace FrenchBulldogsPortal.Services.FrenchBulldogs.Models
{
    using System.Collections.Generic;

    public class FrenchBulldogQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int FrenchBulldogsPerPage { get; init; }

        public int TotalFrenchBulldogs { get; init; }

        public IEnumerable<FrenchBulldogServiceModel> FrenchBulldogs { get; init; }
    }
}
