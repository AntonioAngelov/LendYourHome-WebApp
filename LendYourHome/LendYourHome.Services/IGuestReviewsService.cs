namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Reviews;

    public interface IGuestReviewsService
    {
        IEnumerable<ReceivedGuestReviewServiceModel> GetReceivedReviews(string userId);
    }
}
