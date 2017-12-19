namespace LendYourHome.Application.Areas.Admin.Models
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.AdminServices.AdminServiceModels;

    public class BannedUsersViewModel
    {

        public PageListingModel PageListingData { get; set; }

        public IEnumerable<BannedUserAdminServiceModel> BannedUsers { get; set; }
    }
}
