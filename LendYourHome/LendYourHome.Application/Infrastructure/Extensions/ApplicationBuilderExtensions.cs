namespace LendYourHome.Application.Infrastructure.Extensions
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<LendYourHomeDbContext>()
                    .Database
                    .Migrate();

                //    var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                //    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //    Task
                //        .Run(async () =>
                //        {
                //            var adminName = WebConstants.AdminRole;

                //            var roles = new[]
                //            {
                //                adminName,
                //                WebConstants.BlogAuthor,
                //                WebConstants.Student,
                //                WebConstants.Trainer
                //            };

                //            foreach (var role in roles)
                //            {
                //                var hasRoles = await roleManager.RoleExistsAsync(role);

                //                if (!hasRoles)
                //                {

                //                    await roleManager.CreateAsync(new IdentityRole
                //                    {
                //                        Name = role
                //                    });
                //                }
                //            }

                //            var adminUser = await userManager.FindByNameAsync(adminName);

                //            if (adminUser == null)
                //            {
                //                adminUser = new User
                //                {
                //                    UserName = adminName,
                //                    Email = "admin@admin.admin",
                //                    Name = "admin",
                //                    BirthDate = DateTime.UtcNow
                //                };

                //                await userManager.CreateAsync(adminUser, "admin123");

                //                await userManager.AddToRoleAsync(adminUser, adminName);
                //            }
                //        })
                //    .Wait();
                //}

                return app;
            }
        }
    }
}
