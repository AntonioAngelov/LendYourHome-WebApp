namespace LendYourHome.Services.Implementations
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<UserForReviewServiceModel> WaitingForReview(string hostId)
            => this.db.Users
                .Where(u => u.GuestReviewsReceived.Count(r => r.HostId == hostId) <
                            u.BookingsMade.Count(b => b.Home.OwnerId == hostId && b.CheckOutDate <= DateTime.UtcNow && b.IsApproved))
                .ProjectTo<UserForReviewServiceModel>()
                .ToList();

        public string GetName(string userId)
            => this.db.Users
                .Find(userId)
                .UserName;

        public IEnumerable<TopGuestServiceModel> TopSixGuestsByAverageRating()
            => this.db.Users
                .OrderByDescending(u => u.GuestReviewsReceived.Sum(r => r.Evaluation) / (double)u.GuestReviewsReceived.Count)
                .Take(6)
                .ProjectTo<TopGuestServiceModel>()
                .ToList();

        public bool UsernameIsTaken(string userId, string userName)
            => this.db.Users
            .Any(u => u.UserName == userName && u.Id != userId);

        public UserEditServiceModel GetForEdit(string userId)
            => this.db.Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserEditServiceModel>()
                .FirstOrDefault();

        public void Edit(string userId, string userName, string additionaInfo, string address, string profilePictureUrl)
        {
            var user = this.db.Users.Find(userId);

            user.UserName = userName;
            user.AdditionalInformation = additionaInfo;
            user.Address = address;

            if (profilePictureUrl != null)
            {
                user.ProfilePictureUrl = profilePictureUrl;
            }

            this.db.SaveChanges();
        }
    }
}
