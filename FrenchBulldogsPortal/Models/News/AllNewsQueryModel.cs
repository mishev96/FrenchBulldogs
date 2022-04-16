namespace FrenchBulldogsPortal.Models.News
{
    using System.Collections.Generic;
    using FrenchBulldogsPortal.Services.News.Models;

    public class AllNewsQueryModel
    {
        public const int NewsPerPage = 3;

        public string Title { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalNews { get; set; }

        public IEnumerable<NewsServiceModel> News { get; set; }
    }
}
