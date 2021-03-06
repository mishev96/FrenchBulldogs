namespace FrenchBulldogsPortal.Infrastructure
{
    using AutoMapper;
    using FrenchBulldogsPortal.Data.Models;
    using FrenchBulldogsPortal.Models.FrenchBulldogs;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;
    using FrenchBulldogsPortal.Services.News.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, FrenchBulldogCategoryServiceModel>();
            this.CreateMap<Color, FrenchBulldogColorServiceModel>();

            this.CreateMap<FrenchBulldog, LatestFrenchBulldogServiceModel>();
            this.CreateMap<FrenchBulldogDetailsServiceModel, FrenchBulldogFormModel>();

            this.CreateMap<FrenchBulldog, FrenchBulldogServiceModel>()
                .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));

            this.CreateMap<FrenchBulldog, FrenchBulldogDetailsServiceModel>()
                .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Breeder.UserId))
                .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name))
                .ForMember(c => c.ColorName, cfg => cfg.MapFrom(c => c.Color.Name));

            this.CreateMap<News, NewsServiceModel>();
        }
    }
}
