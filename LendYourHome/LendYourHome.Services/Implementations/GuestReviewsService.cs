namespace LendYourHome.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
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
                .OrderByDescending(r => r.SubmitDate)
                .ProjectTo<ReceivedGuestReviewServiceModel>()
                .ToList();

        public bool CanCreateReview(string hostId, string guestid)
            => this.db.Users
                .Any(u => u.Id == hostId
                            && u.GuestReviewsMade.Count(r => r.EvaluatedGuestId == guestid)
                            < u.Home.Bookings.Count(b =>
                                b.IsApproved && b.CheckOutDate <= DateTime.UtcNow && b.GuestId == guestid));

        public void Create(string hostId, string guestId, int evaluation, string additionalThoughts, string title)
        {
            var review = new GuestReview
            {
                HostId = hostId,
                Title = title,
                AdditionalThoughts = additionalThoughts,
                EvaluatedGuestId = guestId,
                Evaluation = evaluation,
                SubmitDate = DateTime.UtcNow
            };

            this.db.GuestReviews.Add(review);
            this.db.SaveChanges();
        }

        public IEnumerable<DoneGuestReviewServiceModel> Done(string hostId)
            => this.db.GuestReviews
                .Where(r => r.HostId == hostId)
                .ProjectTo<DoneGuestReviewServiceModel>()
                .OrderByDescending(r => r.SubmitDate)
                .ToList();
    }
}
