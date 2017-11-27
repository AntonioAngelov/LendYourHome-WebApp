namespace LendYourHome.Services.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly LendYourHomeDbContext db;

        public UserService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public string GetUserProfilePicture(string userId)
            => this.db.Users
                .Find(userId)
                .ProfilePictureUrl;
    }
}
