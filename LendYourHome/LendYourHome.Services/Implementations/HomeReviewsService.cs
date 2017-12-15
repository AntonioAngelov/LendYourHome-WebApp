namespace LendYourHome.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using ServiceModels.Reviews;

    public class HomeReviewsService : IHomeReviewsService
    {
        private readonly LendYourHomeDbContext db;

        public HomeReviewsService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public bool CanCreateReview(string userId, int homeId)
            => this.db.Homes
                .Any(h => h.Id == homeId &&
                h.Bookings.Count(b => b.GuestId == userId &&
                                                b.IsApproved &&
                                                b.CheckOutDate <= DateTime.UtcNow)
                > h.Reviews.Count(r => r.EvaluatingGuestId == userId));

        public void Create(string userId, int homeId, int evaluation, string additionalThoughts, string title)
        {
            var homeReview = new HomeReview
            {
                EvaluatingGuestId = userId,
                HomeId = homeId,
                AdditionalThoughts = additionalThoughts,
                Evaluation = evaluation,
                SubmitDate = DateTime.UtcNow,
                Title = title
            };

            this.db.HomeReviews.Add(homeReview);
            this.db.SaveChanges();
        }

        public IEnumerable<DoneHomeReviewServiceModel> Done(int pageNumber,
            int pageSize, 
            string userId)
            => this.db.HomeReviews
                .Where(r => r.EvaluatingGuestId == userId)
                .OrderByDescending(r => r.SubmitDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<DoneHomeReviewServiceModel>()
                .ToList();

        public IEnumerable<ReceivedHomeReviewServiceModel> GetReceivedReviews(int homeId)
            => this.db.HomeReviews
                .Where(r => r.HomeId == homeId)
                .OrderByDescending(r => r.SubmitDate)
                .ProjectTo<ReceivedHomeReviewServiceModel>()
                .ToList();

        public int TotalDoneByUser(string userId)
            => this.db.HomeReviews
                .Count(r => r.EvaluatingGuestId == userId);
    }
}
