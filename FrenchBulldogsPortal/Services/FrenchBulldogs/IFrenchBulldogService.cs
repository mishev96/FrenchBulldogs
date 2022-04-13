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
            int frenchBulldogsPerPage = int.MaxValue);

        IEnumerable<LatestFrenchBulldogServiceModel> Latest();

        FrenchBulldogDetailsServiceModel Details(int frenchBulldogId);

        int Create(
            string name,
            string color,
            string description,
            string imageUrl,
            int age,
            int categoryId,
            int dealerId);

        bool Edit(
            int frenchBulldogId,
            string name,
            string color,
            string description,
            string imageUrl,
            int age,
            int categoryId);

        IEnumerable<FrenchBulldogServiceModel> ByUser(string userId);

        bool IsByDealer(int frenchBulldogId, int dealerId);

        void ChangeVisility(int frenchBulldogId);

        IEnumerable<string> AllNames();

        IEnumerable<FrenchBulldogCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
