namespace LendYourHome.Application.Areas.Guest.Models.Reviews
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.ServiceModels.Reviews;

    public class DoneHomeReviewsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<DoneHomeReviewServiceModel> Reviews { get; set; }
    }
}
