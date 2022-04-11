namespace FrenchBulldogsPortal.Services.FrenchBulldogs
{
    using System.Collections.Generic;
    using FrenchBulldogsPortal.Models;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;

    public interface IFrenchBulldogService
    {
        FrenchBulldogQueryServiceModel All(
            string name = null,
            string searchTerm = null,
            FrenchBulldogSorting sorting = FrenchBulldogSorting.DateCreated, 
            int currentPage = 1,
            int carsPerPage = int.MaxValue);

        IEnumerable<LatestFrenchBulldogServiceModel> Latest();

        FrenchBulldogDetailsServiceModel Details(int carId);

        int Create(
            string name,
            string color,
            string description,
            string imageUrl,
            int age,
            int categoryId,
            int dealerId);

        bool Edit(
            int carId,
            string name,
            string color,
            string description,
            string imageUrl,
            int age,
            int categoryId);

        IEnumerable<FrenchBulldogServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        void ChangeVisility(int carId);

        IEnumerable<string> AllNames();

        IEnumerable<FrenchBulldogCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
