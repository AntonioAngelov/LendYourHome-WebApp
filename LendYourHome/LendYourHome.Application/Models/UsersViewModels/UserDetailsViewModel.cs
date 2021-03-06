﻿namespace LendYourHome.Application.Models.UsersViewModels
{
    using System.Collections.Generic;
    using Services.ServiceModels.Reviews;
    using Services.ServiceModels.Users;

    public class UserDetailsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public UserDetailsServiceModel UserInfo { get; set; }

        public IEnumerable<ReceivedGuestReviewServiceModel> ReviewsReceived { get; set; }
    }
}
