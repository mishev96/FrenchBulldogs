namespace FrenchBulldogsPortal.Services.Statistics
{
    using System.Linq;
    using FrenchBulldogsPortal.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly FrenchBulldogsDbContext data;

        public StatisticsService(FrenchBulldogsDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalFrenchulldogs = this.data.FrenchBulldogs.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalFrenchBulldogs = totalFrenchulldogs,
                TotalUsers = totalUsers
            };
        }
    }
}
