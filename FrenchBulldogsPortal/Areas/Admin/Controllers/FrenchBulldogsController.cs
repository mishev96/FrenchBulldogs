namespace FrenchBulldogsPortal.Areas.Admin.Controllers
{
    using FrenchBulldogsPortal.Services.FrenchBulldogs;
    using Microsoft.AspNetCore.Mvc;

    public class FrenchBulldogsController : AdminController
    {
        private readonly IFrenchBulldogService frenchBulldogs;

        public FrenchBulldogsController(IFrenchBulldogService frenchBulldogs) => this.frenchBulldogs = frenchBulldogs;

        public IActionResult All()
        {
            var frenchBulldogs = this.frenchBulldogs
                .All()
                .FrenchBulldogs;

            return View(frenchBulldogs);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.frenchBulldogs.ChangeVisility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
