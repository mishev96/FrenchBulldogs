namespace FrenchBulldogs
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using FrenchBulldogs.Contracts;
    using FrenchBulldogs.Data;
    using FrenchBulldogs.Data.Common;
    using FrenchBulldogs.Services;
    using System.Threading.Tasks;

    public class Startup
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection.Add<IUserService, UserService>()
                .Add<FrenchBulldogDbContext>()
                .Add<IRepository, Repository>()
                .Add<IValidationService, ValidationService>()
                .Add<IFrenchBulldogService, FrenchBulldogService>();

            await server.Start();
        }
    }
}
