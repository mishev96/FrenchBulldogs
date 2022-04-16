namespace FrenchBulldogsPortal.Infrastructure.Extensions
{
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;
    using FrenchBulldogsPortal.Services.News.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IFrenchBulldogModel frenchBulldog)
            => frenchBulldog.Name + "-" + frenchBulldog.ColorName + "-" + frenchBulldog.Age;

        public static string GetNewsInformation(this INewsModel sinlgeNews)
            => sinlgeNews.Title + "<br>" + sinlgeNews.Content;
    }
}
