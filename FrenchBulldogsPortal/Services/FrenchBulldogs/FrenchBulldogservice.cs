namespace FrenchBulldogsPortal.Services.FrenchBulldogs
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FrenchBulldogsPortal.Data;
    using FrenchBulldogsPortal.Data.Models;
    using FrenchBulldogsPortal.Models;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models; 

    public class FrenchBulldogservice : IFrenchBulldogService
    {
        private readonly FrenchBulldogsDbContext data;
        private readonly IConfigurationProvider mapper;

        public FrenchBulldogservice(FrenchBulldogsDbContext data, IMapper mapper) 
		{
			this.data = data;
			this.mapper = mapper.ConfigurationProvider;
		}

        public FrenchBulldogQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            FrenchBulldogSorting sorting = FrenchBulldogSorting.DateCreated,
            int currentPage = 1,
            int frenchBulldogsPerPage = int.MaxValue)
        {
            var frenchBulldogsQuery = this.data.FrenchBulldogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                frenchBulldogsQuery = frenchBulldogsQuery.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                frenchBulldogsQuery = frenchBulldogsQuery.Where(c =>
                    (c.Name + " " + c.Color.Name).ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            frenchBulldogsQuery = sorting switch
            {
                FrenchBulldogSorting.Age => frenchBulldogsQuery.OrderByDescending(c => c.Age),
                FrenchBulldogSorting.NameAndColor => frenchBulldogsQuery.OrderBy(c => c.Name).ThenBy(c => c.Color.Name),
                FrenchBulldogSorting.DateCreated or _ => frenchBulldogsQuery.OrderByDescending(c => c.Id)
            };

            var totalFrenchBulldogs = frenchBulldogsQuery.Count();

            var frenchBulldogs = GetFrenchBulldogs(frenchBulldogsQuery
                .Skip((currentPage - 1) * frenchBulldogsPerPage)
                .Take(frenchBulldogsPerPage));

            return new FrenchBulldogQueryServiceModel
            {
                TotalFrenchBulldogs = totalFrenchBulldogs,
                CurrentPage = currentPage,
                FrenchBulldogsPerPage = frenchBulldogsPerPage,
                FrenchBulldogs = frenchBulldogs
            };
        }

        public IEnumerable<LatestFrenchBulldogServiceModel> Latest()
            => this.data
                .FrenchBulldogs
                .OrderByDescending(c => c.Id)
                .ProjectTo<LatestFrenchBulldogServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public FrenchBulldogDetailsServiceModel Details(int id)
            => this.data
                .FrenchBulldogs
                .Where(c => c.Id == id)
                .ProjectTo<FrenchBulldogDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(string name, int colorId, string description, string imageUrl, int age, int categoryId, int breederId)
        {
            var frenchBulldogData = new FrenchBulldog
            {
                Name = name,
                ColorId = colorId,
                Description = description,
                ImageUrl = imageUrl,
                Age = age,
                CategoryId = categoryId,
                BreederId = breederId,
            };

            this.data.FrenchBulldogs.Add(frenchBulldogData);
            this.data.SaveChanges();

            return frenchBulldogData.Id;
        }

        public bool Edit(
            int id, 
            string name, 
            int colorId, 
            string description, 
            string imageUrl, 
            int age, 
            int categoryId)
        {
            var frenchBulldogData = this.data.FrenchBulldogs.Find(id);

            if (frenchBulldogData == null)
            {
                return false;
            }

            frenchBulldogData.Name = name;
            frenchBulldogData.ColorId = colorId;
            frenchBulldogData.Description = description;
            frenchBulldogData.ImageUrl = imageUrl;
            frenchBulldogData.Age = age;
            frenchBulldogData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<FrenchBulldogServiceModel> ByUser(string userId)
            => GetFrenchBulldogs(this.data
                .FrenchBulldogs
                .Where(c => c.Breeder.UserId == userId));

        public bool IsByBreeder(int frenchBulldogId, int breederId)
            => this.data
                .FrenchBulldogs
                .Any(c => c.Id == frenchBulldogId && c.BreederId == breederId);

        public void ChangeVisility(int frenchBulldogId)
        {
            var frenchBulldog = this.data.FrenchBulldogs.Find(frenchBulldogId);

            this.data.SaveChanges();
        }

        public IEnumerable<string> AllNames()
            => this.data
                .FrenchBulldogs
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<FrenchBulldogCategoryServiceModel> AllCategories()
            => this.data
                .Categories
                .ProjectTo<FrenchBulldogCategoryServiceModel>(this.mapper)
                .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        public IEnumerable<FrenchBulldogColorServiceModel> AllColors()
            => this.data
                .Colors
                .ProjectTo<FrenchBulldogColorServiceModel>(this.mapper)
                .ToList();

        public bool ColorExists(int colorId)
            => this.data
                .Colors
                .Any(c => c.Id == colorId);

        public string GetColorById(int colorId)
            => this.data
        .Colors
        .Where(c => c.Id == colorId)
        .FirstOrDefault().Name;

        private IEnumerable<FrenchBulldogServiceModel> GetFrenchBulldogs(IQueryable<FrenchBulldog> frenchBulldogsQuery)
            => frenchBulldogsQuery
                .ProjectTo<FrenchBulldogServiceModel>(this.mapper)
                .ToList();
    }
}
