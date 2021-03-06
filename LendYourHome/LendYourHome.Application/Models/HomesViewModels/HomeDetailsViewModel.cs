﻿namespace LendYourHome.Application.Models.HomesViewModels
{
    using System.Collections.Generic;
    using Services.ServiceModels.Homes;
    using Services.ServiceModels.Reviews;

    public class HomeDetailsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public HomeDetailsServiceModel HomeInfo { get; set; }

        public IEnumerable<ReceivedHomeReviewServiceModel> Reviews { get; set; }
    }
}
