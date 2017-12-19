namespace LendYourHome.Application.Areas.Host.Models.Reviews
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.ServiceModels.Reviews;

    public class DoneGuestReviewsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<DoneGuestReviewServiceModel> Reviews { get; set; }
    }
}
