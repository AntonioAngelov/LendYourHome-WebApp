namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Reviews;

    public interface IGuestReviewsService
    {
        IEnumerable<ReceivedGuestReviewServiceModel> GetReceivedReviews(string userId);

        bool CanCreateReview(string hostId, string guestid);

        void Create(
            string hostId,
            string guestId,
            int evaluation,
            string additionalThoughts,
            string title);

        IEnumerable<DoneGuestReviewServiceModel> Done(string hostId);
    }
}
