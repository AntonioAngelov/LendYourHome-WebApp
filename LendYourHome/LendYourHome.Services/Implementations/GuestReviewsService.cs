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

        public IEnumerable<ReceivedGuestReviewServiceModel> GetReceivedReviews(string userId, int pageNumber, int pageSize)
            => this.db.GuestReviews
                .Where(r => r.EvaluatedGuestId == userId)
                .OrderByDescending(r => r.SubmitDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

        public IEnumerable<DoneGuestReviewServiceModel> Done(int pageNumber,
            int pageSize, 
            string hostId)
            => this.db.GuestReviews
                .Where(r => r.HostId == hostId)
                .OrderByDescending(r => r.SubmitDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<DoneGuestReviewServiceModel>()
                .ToList();

        public int TotalDoneByUser(string userId)
            => this.db
            .GuestReviews
            .Count(r => r.HostId == userId);

        public int TotalReceivedByUser(string userId)
            => this.db.GuestReviews
                .Count(r => r.EvaluatedGuestId == userId);
    }
}
