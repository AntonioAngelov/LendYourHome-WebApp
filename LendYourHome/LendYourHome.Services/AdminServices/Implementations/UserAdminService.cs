namespace LendYourHome.Services.AdminServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdminServiceModels;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class UserAdminService : IUserAdminService
    {
        private readonly LendYourHomeDbContext db;

        public UserAdminService(LendYourHomeDbContext db)
        {
            this.db = db;
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

            if (user.Home != null)
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
    }
}
