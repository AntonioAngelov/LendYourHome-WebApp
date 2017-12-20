namespace LendYourHome.Services.AdminServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminServiceModels;
    using AutoMapper.QueryableExtensions;
    using Common.Constants;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserAdminService : IUserAdminService
    {
        private readonly LendYourHomeDbContext db;
        private readonly UserManager<User> userManager;

        public UserAdminService(LendYourHomeDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IEnumerable<ActiveUserAdminServiceModel> ActiveUsers(
            int pageNumber,
            int pageSize)
            => this.db
                .Users
                .Where(u => u.BanEndDate == null ||
                            u.BanEndDate.Value < DateTime.UtcNow)
                .OrderByDescending(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ActiveUserAdminServiceModel>()
                .ToList();

        public IEnumerable<BannedUserAdminServiceModel> BannedUsers(
            int pageNumber,
            int pageSize)
            => this.db
                .Users
                .Where(u => u.BanEndDate != null &&
                            u.BanEndDate.Value >= DateTime.UtcNow)
                .OrderBy(u => u.BanEndDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BannedUserAdminServiceModel>()
                .ToList();

        public void BannUser(string userId, DateTime? banEndDate)
        {
            var user = this.db.Users
                .Include(u => u.Home)
                .FirstOrDefault(u => u.Id == userId);

            if (user.Home != null && banEndDate >= DateTime.UtcNow)
            {
                user.Home.IsActiveOffer = false;
            }

            user.BanEndDate = banEndDate;

            this.db.SaveChanges();

        }

        public int TotalActive()
            => this.db
                .Users
                .Count(u => u.BanEndDate == null ||
                            u.BanEndDate.Value < DateTime.UtcNow);

        public int TotalBanned()
            => this.db
                .Users
                .Count(u => u.BanEndDate != null &&
                            u.BanEndDate.Value >= DateTime.UtcNow);

        public void MakeAdmin(string userId)
        {
            var user = this.db.Users.Find(userId);

            Task
                .Run(async () =>
                {
                    await this.userManager.AddToRoleAsync(user, ApplicationConstants.AdminRole);
                })
                .Wait();
        }
    }
}
