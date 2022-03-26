using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FrenchBulldogs.Contracts;

namespace FrenchBulldogs.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IFrenchBulldogService bulldogService;

        public HomeController(Request request, IUserService _userService, IFrenchBulldogService _bulldogService) : base(request)
        {
            userService = _userService;
            bulldogService = _bulldogService;
        }

        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                string username = userService.GetUsername(User.Id);

                var model = new
                {
                    Username = username,
                    IsAuthenticated = true,
                    FrenchBulldogs = bulldogService.GetBulldogs()
                };

                return View(model, "/FrenchBulldogs/ForSale");
            }

            return this.View(new { IsAuthenticated = false });
        }
    }
}