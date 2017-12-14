namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Reviews;

    public interface IHomeReviewsService
    {
        bool CanCreateReview(string userId, int homeId);

        void Create(
            string userId,
            int homeId,
            int evaluation,
            string additionalThoughts,
            string title);

        IEnumerable<DoneHomeReviewServiceModel> Done(string userId);

        IEnumerable<ReceivedHomeReviewServiceModel> GetReceivedReviews(int homeId);
    }
}
