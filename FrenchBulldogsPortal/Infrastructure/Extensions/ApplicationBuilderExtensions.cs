namespace FrenchBulldogsPortal.Infrastructure.Extensions
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FrenchBulldogsPortal.Data;
	using FrenchBulldogsPortal.Data.Models;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	using static FrenchBulldogsPortal.Areas.Admin.AdminConstants;

	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder PrepareDatabase(
			this IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			var services = serviceScope.ServiceProvider;

			MigrateDatabase(services);

			SeedCategories(services);
			SeedColors(services);
			SeedAdministrator(services);

			return app;
		}

		private static void MigrateDatabase(IServiceProvider services)
		{
			var data = services.GetRequiredService<FrenchBulldogsDbContext>();

			data.Database.Migrate();
		}

		private static void SeedCategories(IServiceProvider services)
		{
			var data = services.GetRequiredService<FrenchBulldogsDbContext>();

			if (data.Categories.Any())
			{
				return;
			}

			data.Categories.AddRange(new[]
			{
				new Category { Name = "Sale" },
				new Category { Name = "Stud" }
			});

			data.SaveChanges();
		}

		private static void SeedColors(IServiceProvider services)
		{
			var data = services.GetRequiredService<FrenchBulldogsDbContext>();

			if (data.Colors.Any())
			{
				return;
			}

			data.Colors.AddRange(new[]
			{
				new Color { Name = "Black" },
				new Color { Name = "Black and Tan" },
				new Color { Name = "Black and Fawn" },
				new Color { Name = "Black and White" },
				new Color { Name = "Blue (Grey)" },
				new Color { Name = "Blue and Tan" },
				new Color { Name = "Blue and Fawn" },
				new Color { Name = "Blue Merle" },
				new Color { Name = "Brindle" },
				new Color { Name = "Brindle and White" },
				new Color { Name = "Chocolate" },
				new Color { Name = "Chocolate and Tan" },
				new Color { Name = "Cream" },
				new Color { Name = "Cream and White" },
				new Color { Name = "Fawn" },
				new Color { Name = "Fawn and White" },
				new Color { Name = "Fawn Brindle and White" },
				new Color { Name = "Isabella (Lilac)" },
				new Color { Name = "Merle" },
				new Color { Name = "Pied" },
				new Color { Name = "Sable" },
				new Color { Name = "Tan" },
				new Color { Name = "White"},
				new Color { Name = "White and Brindle"},
				new Color { Name = "White and Fawn"}
			});

			data.SaveChanges();
		}

		private static void SeedAdministrator(IServiceProvider services)
		{
			var userManager = services.GetRequiredService<UserManager<User>>();
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			Task
				.Run(async () =>
				{
					if (await roleManager.RoleExistsAsync(AdministratorRoleName))
					{
						return;
					}

					var role = new IdentityRole { Name = AdministratorRoleName };

					await roleManager.CreateAsync(role);

					const string adminEmail = "admin@breed.com";
					const string adminPassword = "admin12";

					var user = new User
					{
						Email = adminEmail,
						UserName = adminEmail,
						FullName = "Admin"
					};

					await userManager.CreateAsync(user, adminPassword);

					await userManager.AddToRoleAsync(user, role.Name);
				})
				.GetAwaiter()
				.GetResult();
		}
	}
}
