using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FrenchBulldogs.Contracts;
using FrenchBulldogs.ViewModels;

namespace FrenchBulldogs.Controllers
{
    public class FrenchBulldogsController : Controller
    {
        private readonly IFrenchBulldogService bulldogService;

        public FrenchBulldogsController(
            Request request,
            IFrenchBulldogService _bulldogService)
            : base(request)
        {
            bulldogService = _bulldogService;
        }

        [Authorize]
        public Response Add()
        {
            return View(new { IsAuthenticated = true });
        }

        [HttpPost]
        [Authorize]
        public Response Add(CreateViewModel model)
        {
            var (created, error) = bulldogService.Add(model);

            if (!created)
            {
                return View(new { ErrorMessage = error }, "/Error");
            }

            return Redirect("/");
        }
    }
}
