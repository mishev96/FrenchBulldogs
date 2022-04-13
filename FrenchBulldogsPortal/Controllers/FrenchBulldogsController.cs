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

	public class FrenchBulldogsController : Controller
    {
        private readonly IFrenchBulldogService frenchBulldogs;
        private readonly IBreederService breeders;
        private readonly IMapper mapper;

        public FrenchBulldogsController(
            IFrenchBulldogService frenchBulldogs,
            IBreederService breeders, 
            IMapper mapper)
        {
            this.frenchBulldogs = frenchBulldogs;
            this.breeders = breeders;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllFrenchBulldogsQueryModel query)
        {
            var queryResult = this.frenchBulldogs.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllFrenchBulldogsQueryModel.FrenchBulldogsPerPage);

            var frenchBulldogNames = this.frenchBulldogs.AllNames();

            query.Names = frenchBulldogNames;
            query.TotalFrenchBulldogs = queryResult.TotalFrenchBulldogs;
            query.FrenchBulldogs = queryResult.FrenchBulldogs;

            return View(query);
        }
        
        [Authorize]
        public IActionResult Mine()
        {
            var myFrenchBulldogs = this.frenchBulldogs.ByUser(this.User.Id());

            return View(myFrenchBulldogs);
        }

        public IActionResult Details(int id, string information)
        {
            var frenchBulldog = this.frenchBulldogs.Details(id);

            if (information != frenchBulldog.GetInformation())
            {
                return BadRequest();
            }

            return View(frenchBulldog);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.breeders.IsBreeder(this.User.Id()))
            {
                return RedirectToAction(nameof(BreedersController.Become), "Breeders");
            }

            return View(new FrenchBulldogFormModel
            {
                Categories = this.frenchBulldogs.AllCategories(),
                Colors = this.frenchBulldogs.AllColors()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(FrenchBulldogFormModel frenchBulldog)
        {
            var breederId = this.breeders.IdByUser(this.User.Id());

            if (breederId == 0)
            {
                return RedirectToAction(nameof(BreedersController.Become), "Breeders");
            }

            if (!this.frenchBulldogs.CategoryExists(frenchBulldog.CategoryId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.CategoryId), "Category does not exist.");
            }

            if (!this.frenchBulldogs.ColorExists(frenchBulldog.ColorId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.ColorId), "Color does not exist.");
            }

            if (!ModelState.IsValid)
            {
                frenchBulldog.Categories = this.frenchBulldogs.AllCategories();
                frenchBulldog.Colors = this.frenchBulldogs.AllColors();

                return View(frenchBulldog);
            }

            var frenchBulldogId = this.frenchBulldogs.Create(
                frenchBulldog.Name,
                frenchBulldog.ColorId,
                frenchBulldog.Description,
                frenchBulldog.ImageUrl,
                frenchBulldog.Age,
                frenchBulldog.CategoryId,
                breederId);

            frenchBulldog.ColorName = this.frenchBulldogs.GetColorById(frenchBulldog.ColorId);
            TempData[GlobalMessageKey] = "You frenchBulldog was added and is awaiting for approval!";
            
            return RedirectToAction(nameof(Details), new { id = frenchBulldogId, information = frenchBulldog.GetInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.breeders.IsBreeder(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(BreedersController.Become), "Breeders");
            }

            var frenchBulldog = this.frenchBulldogs.Details(id);

            if (frenchBulldog.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var frenchBulldogForm = this.mapper.Map<FrenchBulldogFormModel>(frenchBulldog);

            frenchBulldogForm.Categories = this.frenchBulldogs.AllCategories();
            frenchBulldogForm.Colors = this.frenchBulldogs.AllColors();

            return View(frenchBulldogForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, FrenchBulldogFormModel frenchBulldog)
        {
            var breederId = this.breeders.IdByUser(this.User.Id());

            if (breederId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(BreedersController.Become), "Breeders");
            }

            if (!this.frenchBulldogs.CategoryExists(frenchBulldog.CategoryId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.CategoryId), "Category does not exist.");
            }

            if (!this.frenchBulldogs.ColorExists(frenchBulldog.ColorId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.ColorId), "Color does not exist.");
            }

            if (!ModelState.IsValid)
            {
                frenchBulldog.Categories = this.frenchBulldogs.AllCategories();
                frenchBulldog.Colors = this.frenchBulldogs.AllColors();

                return View(frenchBulldog);
            }

            if (!this.frenchBulldogs.IsByBreeder(id, breederId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.frenchBulldogs.Edit(
                id,
                frenchBulldog.Name,
                frenchBulldog.ColorId,
                frenchBulldog.Description,
                frenchBulldog.ImageUrl,
                frenchBulldog.Age,
                frenchBulldog.CategoryId);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"You frenchBulldog was edited{(this.User.IsAdmin() ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = frenchBulldog.GetInformation() });
        }
    }
}
