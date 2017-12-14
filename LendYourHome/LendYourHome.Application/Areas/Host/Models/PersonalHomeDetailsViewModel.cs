namespace LendYourHome.Application.Areas.Host.Models
{
    using System.Collections.Generic;
    using Services.ServiceModels.Homes;
    using Services.ServiceModels.Reviews;

    public class PersonalHomeDetailsViewModel
    {
        public PersonalHomeDetailsServiceModel HomeInfo { get; set; }

        public IEnumerable<ReceivedHomeReviewServiceModel> Reviews { get; set; }
    }
}
