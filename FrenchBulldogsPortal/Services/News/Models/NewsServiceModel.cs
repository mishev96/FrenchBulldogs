namespace FrenchBulldogsPortal.Services.News.Models
{
    public class NewsServiceModel : INewsModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }
    }
}
