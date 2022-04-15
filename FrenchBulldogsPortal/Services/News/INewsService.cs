namespace FrenchBulldogsPortal.Services.News
{
    using System.Collections.Generic;
    using FrenchBulldogsPortal.Models;
    using FrenchBulldogsPortal.Services.News.Models;

    public interface INewsService
    {
        NewsQueryServiceModel All(
            int currentPage = 1,
            int newsPerPage = int.MaxValue);

        IEnumerable<NewsServiceModel> Latest();

        NewsServiceModel Details(int newsId);

        int Create(
            string title,
            string content);

        bool Edit(
            int newsId,
            string title,
            string content);

    }
}
