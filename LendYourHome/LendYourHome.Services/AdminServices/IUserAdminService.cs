﻿namespace LendYourHome.Services.AdminServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AdminServiceModels;

    public interface IUserAdminService
    {
        IEnumerable<ActiveUserAdminServiceModel> ActiveUsers(
            int pageNumber,
            int pageSize);

        IEnumerable<BannedUserAdminServiceModel> BannedUsers(
            int pageNumber,
            int pageSize);

        void BannUser(string userId, DateTime? banEndDate);

        int TotalActive();

        int TotalBanned();

        void MakeAdmin(string userId);
    }
}
