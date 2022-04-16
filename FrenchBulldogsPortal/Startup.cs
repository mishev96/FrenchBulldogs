namespace FrenchBulldogsPortal
{
    using FrenchBulldogsPortal.Controllers;
    using FrenchBulldogsPortal.Data;
    using FrenchBulldogsPortal.Data.Models;
    using FrenchBulldogsPortal.Infrastructure.Extensions;
    using FrenchBulldogsPortal.Services.Breeders;
	using FrenchBulldogsPortal.Services.FrenchBulldogs;
	using FrenchBulldogsPortal.Services.News;
	using FrenchBulldogsPortal.Services.Statistics;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<FrenchBulldogsDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FrenchBulldogsDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IFrenchBulldogService, FrenchBulldogservice>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IBreederService, BreederService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();

                    endpoints.MapControllerRoute(
                        name: "French Bulldog Details",
                        pattern: "/FrenchBulldogs/Details/{id}/{information}",
                        defaults: new 
                        { 
                            controller = typeof(FrenchBulldogsController).GetControllerName(), 
                            action = nameof(FrenchBulldogsController.Details) 
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
