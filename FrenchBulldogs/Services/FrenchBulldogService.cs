using FrenchBulldogs.Contracts;
using FrenchBulldogs.Data.Common;
using FrenchBulldogs.Data.Models;
using FrenchBulldogs.ViewModels;

namespace FrenchBulldogs.Services
{
    public class FrenchBulldogService : IFrenchBulldogService
    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;

        public FrenchBulldogService(
            IRepository _repo,
            IValidationService _validationService)
        {
            repo = _repo;
            validationService = _validationService;
        }
        public (bool created, string error) Add(CreateViewModel model)
        {
            bool created = false;
            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            var bulldog = new FrenchBulldog()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Age = model.Age,
                Color = model.Color,
                Description = model.Description
            };

            try
            {
                repo.Add(bulldog);
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
                error = "Could not save product";
            }

            return (created, error);
        }

        public IEnumerable<FrenchBulldogListVeiwModel> GetBulldogs()
        {
            return repo.All<FrenchBulldog>()
                .Select(p => new FrenchBulldogListVeiwModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    Color = p.Color
                })
                .ToList();
        }
    }
}