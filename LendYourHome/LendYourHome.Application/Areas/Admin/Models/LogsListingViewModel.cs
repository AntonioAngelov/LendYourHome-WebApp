namespace LendYourHome.Application.Areas.Admin.Models
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.AdminServices.AdminServiceModels;

    public class LogsListingViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<LogAdminServiceModel> Logs { get; set; }
    }
}
