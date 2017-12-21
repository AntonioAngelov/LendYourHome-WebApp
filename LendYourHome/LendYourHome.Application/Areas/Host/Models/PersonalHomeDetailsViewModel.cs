namespace LendYourHome.Application.Areas.Host.Models
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.ServiceModels.Homes;
    using Services.ServiceModels.Reviews;

    public class PersonalHomeDetailsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public PersonalHomeDetailsServiceModel HomeInfo { get; set; }

        public IEnumerable<ReceivedHomeReviewServiceModel> Reviews { get; set; }
    }
}
