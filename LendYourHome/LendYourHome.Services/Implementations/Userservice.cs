namespace LendYourHome.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using ServiceModels.Users;

    public class UserService : IUserService
    {
        private readonly LendYourHomeDbContext db;
        private readonly UserManager<User> userManager;

        public UserService(LendYourHomeDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public string GetUserProfilePicture(string userId)
            => this.db.Users
                .Find(userId)
                .ProfilePictureUrl;

        public bool Exists(string id)
            => this.db.Users
                .Any(u => u.Id == id);
      
        public UserDetailsServiceModel Details(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDetailsServiceModel>()
                .FirstOrDefault();

        public void AddInRole(string userId, string role)
        {
            var user = this.db.Users
                .Find(userId);

            Task
                .Run(async () =>
                {
                    await this.userManager.AddToRoleAsync(user, role);
                })
                .Wait();
        }
    }
}
