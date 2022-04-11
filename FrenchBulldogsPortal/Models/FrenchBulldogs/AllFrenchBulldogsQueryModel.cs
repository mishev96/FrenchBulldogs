namespace FrenchBulldogsPortal.Models.FrenchBulldogs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;

    public class AllFrenchBulldogsQueryModel
    {
        public const int FrenchBulldogsPerPage = 3;

        public string Name { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public FrenchBulldogSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalFrenchBulldogs { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<FrenchBulldogServiceModel> FrenchBulldogs { get; set; }
    }
}
