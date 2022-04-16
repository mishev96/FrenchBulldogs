namespace FrenchBulldogsPortal.Controllers
{
    using AutoMapper;
    using FrenchBulldogsPortal.Infrastructure.Extensions;
    using FrenchBulldogsPortal.Models.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.Breeders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
	using System.Linq;
	using FrenchBulldogsPortal.Services.News;
	using FrenchBulldogsPortal.Models.News;

	public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly IMapper mapper;

        public NewsController(
            INewsService news,
            IMapper mapper)
        {
            this.news = news;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllNewsQueryModel query)
        {
            var queryResult = this.news.All(
                query.CurrentPage,
                AllNewsQueryModel.NewsPerPage);

            query.TotalNews = queryResult.TotalNews;
            query.News = queryResult.News;

            return View(query);
        }

        public IActionResult Details(int id, string information)
        {
            var singleNews = this.news.Details(id);

            if (information != singleNews.GetNewsInformation())
            {
                return BadRequest();
            }

            return View(singleNews);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new NewsFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(NewsFormModel singleNews)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(singleNews);
            }

            var singleNewsId = this.news.Create(
                singleNews.Title,
                singleNews.Content);
            
            return RedirectToAction(nameof(Details), new { id = singleNewsId, information = singleNews.GetNewsInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var singleNews = this.news.Details(id);

            var singleNewsForm = this.mapper.Map<NewsFormModel>(singleNews);

            return View(singleNewsForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, NewsFormModel singleNews)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(singleNews);
            }

            var edited = this.news.Edit(
                id,
                singleNews.Title,
                singleNews.Content);

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id, information = singleNews.GetNewsInformation() });
        }
    }
}
