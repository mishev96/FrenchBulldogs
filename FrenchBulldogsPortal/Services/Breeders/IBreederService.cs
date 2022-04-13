namespace FrenchBulldogsPortal.Services.Breeders
{
    public interface IBreederService
    {
        public bool IsBreeder(string userId);

        public int IdByUser(string userId);
    }
}
