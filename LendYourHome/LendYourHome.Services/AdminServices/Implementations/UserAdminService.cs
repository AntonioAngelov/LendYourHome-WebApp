namespace LendYourHome.Services.AdminServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AdminServiceModels;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserAdminService : IUserAdminService
    {
        private readonly LendYourHomeDbContext db;
        private readonly UserManager<User> userManager;

        public UserAdminService(LendYourHomeDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IEnumerable<ActiveUserAdminServiceModel> ActiveUsers()
            => this.db
                .Users
                .Where(u => u.LockoutEnabled &&
                            (u.LockoutEnd == null || u.LockoutEnd < DateTime.UtcNow))
                .ProjectTo<ActiveUserAdminServiceModel>()
                .ToList();

        public IEnumerable<ActiveUserAdminServiceModel> BannedUsers()
        {
            return null;
        }

        public async void BannUser(string userId, DateTime banEndDate)
        {
            
        }
    }
}
