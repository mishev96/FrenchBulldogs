namespace FrenchBulldogsPortal.Controllers
{
    using System.Linq;
    using FrenchBulldogsPortal.Data;
    using FrenchBulldogsPortal.Data.Models;
    using FrenchBulldogsPortal.Infrastructure.Extensions;
    using FrenchBulldogsPortal.Models.Breeders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class BreedersController : Controller
    {
        private readonly FrenchBulldogsDbContext data;

        public BreedersController(FrenchBulldogsDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeBreederFormModel breeder)
        {
            var userId = this.User.Id();

            var userIdAlreadyBreeder = this.data
                .Breeders
                .Any(d => d.UserId == userId);

            if (userIdAlreadyBreeder)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(breeder);
            }

            var breederData = new Breeder
            {
                Name = breeder.Name,
                PhoneNumber = breeder.PhoneNumber,
                UserId = userId
            };

            this.data.Breeders.Add(breederData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a breeder!";

            return RedirectToAction(nameof(FrenchBulldogsController.All), "FrenchBulldogs");
        }
    }
}
