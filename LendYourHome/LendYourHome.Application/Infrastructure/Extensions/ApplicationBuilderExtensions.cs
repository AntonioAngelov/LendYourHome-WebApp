namespace LendYourHome.Application.Infrastructure.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Common.Constants;

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

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                try
                {
                    Task
                        .Run(async () =>
                        {
                            var adminName = ApplicationConstants.AdminRole;

                            var roles = new[]
                            {
                                adminName,
                                ApplicationConstants.HostRole
                            };

                            foreach (var role in roles)
                            {
                                var hasRoles = await roleManager.RoleExistsAsync(role);

                                if (!hasRoles)
                                {

                                    await roleManager.CreateAsync(new IdentityRole
                                    {
                                        Name = role
                                    });
                                }
                            }

                            var adminUser = await userManager.FindByNameAsync(adminName);

                            if (adminUser == null)
                            {
                                adminUser = new User
                                {
                                    UserName = adminName,
                                    Email = "admin@admin.admin",
                                    ProfilePictureUrl = DataConstants.DefaultProfilePictureUrl
                                };

                                await userManager.CreateAsync(adminUser, "admin123");

                                await userManager.AddToRoleAsync(adminUser, adminName);
                            }
                        })
                        .Wait();
                }
                catch (Exception ex)
                {
                    
                }
            }

            return app; 
        }
    }
}

