namespace LendYourHome.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using ServiceModels.Reviews;

    public class GuestReviewsService : IGuestReviewsService
    {
        private readonly LendYourHomeDbContext db;

        public GuestReviewsService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ReceivedGuestReviewServiceModel> GetReceivedReviews(string userId)
            => this.db.GuestReviews
                .Where(r => r.EvaluatedGuestId == userId)
                .ProjectTo<ReceivedGuestReviewServiceModel>()
                .ToList();
    }
}
