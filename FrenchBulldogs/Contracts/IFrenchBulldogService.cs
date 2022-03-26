using FrenchBulldogs.ViewModels;

namespace FrenchBulldogs.Contracts
{
    public interface IFrenchBulldogService
    {
        (bool created, string error) Add(CreateViewModel model);

        IEnumerable<FrenchBulldogListVeiwModel> GetBulldogs();
    }
}
