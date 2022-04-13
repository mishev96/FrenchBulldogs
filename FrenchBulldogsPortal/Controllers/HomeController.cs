namespace FrenchBulldogsPortal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FrenchBulldogsPortal.Services.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IFrenchBulldogService frenchBulldogs;
        private readonly IMemoryCache cache;

        public HomeController(
            IFrenchBulldogService frenchBulldogs,
            IMemoryCache cache)
        {
            this.frenchBulldogs = frenchBulldogs;
            this.cache = cache;
        }
        
        public IActionResult Index()
        {
            var latestFrenchBulldogs = this.cache.Get<List<LatestFrenchBulldogServiceModel>>(LatestFrenchBulldogsCacheKey);

            if (latestFrenchBulldogs == null)
            {
                latestFrenchBulldogs = this.frenchBulldogs
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestFrenchBulldogsCacheKey, latestFrenchBulldogs, cacheOptions);
            }

            return View(latestFrenchBulldogs);
        }

        public IActionResult Error() => View();
    }
}
