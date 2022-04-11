namespace FrenchBulldogsPortal.Controllers.Api
{
    using FrenchBulldogsPortal.Models.Api.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/frenchBulldogs")]
    public class CarsApiController : ControllerBase
    {
        private readonly IFrenchBulldogService frenchBulldogs;

        public CarsApiController(IFrenchBulldogService frenchBulldogs) 
            => this.frenchBulldogs = frenchBulldogs;

        [HttpGet]
        public FrenchBulldogQueryServiceModel All([FromQuery] AllFrenchBulldogsApiRequestModel query) 
            => this.frenchBulldogs.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage);
    }
}
