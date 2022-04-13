namespace FrenchBulldogsPortal.Infrastructure.Extensions
{
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IFrenchBulldogModel frenchBulldog)
            => frenchBulldog.Name + "-" + frenchBulldog.ColorName + "-" + frenchBulldog.Age;
    }
}
