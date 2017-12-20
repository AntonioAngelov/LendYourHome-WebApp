namespace LendYourHome.Application.Areas.Admin.Models.Users
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.AdminServices.AdminServiceModels;

    public class ActiveUsersViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<ActiveUserAdminServiceModel> ActiveUsers { get; set; }
    }
}
