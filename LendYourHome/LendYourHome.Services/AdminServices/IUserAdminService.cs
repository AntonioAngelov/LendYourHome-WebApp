namespace LendYourHome.Services.AdminServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AdminServiceModels;
    using Microsoft.AspNetCore.Identity;

    public interface IUserAdminService
    {
        IEnumerable<ActiveUserAdminServiceModel> ActiveUsers();

        IEnumerable<ActiveUserAdminServiceModel> BannedUsers();

        void BannUser(string userId, DateTime banEndDate);
    }
}
