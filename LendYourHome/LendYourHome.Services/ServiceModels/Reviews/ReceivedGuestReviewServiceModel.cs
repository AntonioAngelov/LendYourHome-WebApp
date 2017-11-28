namespace LendYourHome.Services.ServiceModels.Reviews
{
    using System;
    using Common.Mapping;
    using Data.Models;

    public class ReceivedGuestReviewServiceModel : IMapFrom<GuestReview>
    {
        public int Evaluation { get; set; }

        public string Title { get; set; }

        public string AdditionalThoughts { get; set; }

        public DateTime SubmitDate { get; set; }

        public string HostId { get; set; }

        public string EvaluatedGuestId { get; set; }
    }
}
