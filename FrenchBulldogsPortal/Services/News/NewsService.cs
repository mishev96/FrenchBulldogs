namespace FrenchBulldogsPortal.Services.News
{
	using System.Collections.Generic;
	using System.Linq;
	using AutoMapper;
	using AutoMapper.QueryableExtensions;
	using FrenchBulldogsPortal.Data;
	using FrenchBulldogsPortal.Data.Models;
	using FrenchBulldogsPortal.Services.News.Models;

	public class NewService : INewsService
	{
		private readonly FrenchBulldogsDbContext data;
		private readonly IConfigurationProvider mapper;

		public NewService(FrenchBulldogsDbContext data, IMapper mapper)
		{
			this.data = data;
			this.mapper = mapper.ConfigurationProvider;
		}

		public NewsQueryServiceModel All(
			int currentPage = 1,
			int newsPerPage = int.MaxValue)
		{
			var newsQuery = this.data.News.AsQueryable();
			var totalNews = newsQuery.Count();

			var news = GetNews(newsQuery
				.Skip((currentPage - 1) * newsPerPage)
				.Take(newsPerPage));

			return new NewsQueryServiceModel
			{
				TotalNews = totalNews,
				CurrentPage = currentPage,
				NewsPerPage = newsPerPage,
				News = news
			};
		}

		public IEnumerable<NewsServiceModel> Latest()
			=> this.data
				.News
				.OrderByDescending(c => c.Id)
				.ProjectTo<NewsServiceModel>(this.mapper)
				.Take(3)
				.ToList();

		public NewsServiceModel Details(int id)
			=> this.data
				.News
				.Where(c => c.Id == id)
				.ProjectTo<NewsServiceModel>(this.mapper)
				.FirstOrDefault();

		public int Create(string title, string content)
		{
			var newsData = new News
			{
				Title = title,
				Content = content
			};

			this.data.News.Add(newsData);
			this.data.SaveChanges();

			return newsData.Id;
		}

		public bool Edit(
			int id,
			string title,
			string content)
		{
			var newsData = this.data.News.Find(id);

			if (newsData == null)
			{
				return false;
			}

			newsData.Title = title;
			newsData.Content = content;

			this.data.SaveChanges();

			return true;
		}

		private IEnumerable<NewsServiceModel> GetNews(IQueryable<News> newsQuery)
			=> newsQuery
				.ProjectTo<NewsServiceModel>(this.mapper)
				.ToList();
	}
}
