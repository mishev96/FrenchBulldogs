namespace FrenchBulldogsPortal.Controllers
{
    using AutoMapper;
    using FrenchBulldogsPortal.Infrastructure.Extensions;
    using FrenchBulldogsPortal.Models.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class FrenchBulldogsController : Controller
    {
        private readonly IFrenchBulldogService frenchBulldogs;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public FrenchBulldogsController(
            IFrenchBulldogService frenchBulldogs,
            IDealerService dealers, 
            IMapper mapper)
        {
            this.frenchBulldogs = frenchBulldogs;
            this.dealers = dealers;
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
            if (!this.dealers.IsDealer(this.User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new FrenchBulldogFormModel
            {
                Categories = this.frenchBulldogs.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(FrenchBulldogFormModel frenchBulldog)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.frenchBulldogs.CategoryExists(frenchBulldog.CategoryId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                frenchBulldog.Categories = this.frenchBulldogs.AllCategories();

                return View(frenchBulldog);
            }

            var frenchBulldogId = this.frenchBulldogs.Create(
                frenchBulldog.Name,
                frenchBulldog.Color,
                frenchBulldog.Description,
                frenchBulldog.ImageUrl,
                frenchBulldog.Age,
                frenchBulldog.CategoryId,
                dealerId);

            TempData[GlobalMessageKey] = "You frenchBulldog was added and is awaiting for approval!";

            return RedirectToAction(nameof(Details), new { id = frenchBulldogId, information = frenchBulldog.GetInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var frenchBulldog = this.frenchBulldogs.Details(id);

            if (frenchBulldog.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var frenchBulldogForm = this.mapper.Map<FrenchBulldogFormModel>(frenchBulldog);

            frenchBulldogForm.Categories = this.frenchBulldogs.AllCategories();

            return View(frenchBulldogForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, FrenchBulldogFormModel frenchBulldog)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.frenchBulldogs.CategoryExists(frenchBulldog.CategoryId))
            {
                this.ModelState.AddModelError(nameof(frenchBulldog.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                frenchBulldog.Categories = this.frenchBulldogs.AllCategories();

                return View(frenchBulldog);
            }

            if (!this.frenchBulldogs.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.frenchBulldogs.Edit(
                id,
                frenchBulldog.Name,
                frenchBulldog.Color,
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
