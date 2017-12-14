namespace LendYourHome.Services.ServiceModels.Reviews
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class DoneGuestReviewServiceModel : IMapFrom<GuestReview>, IHaveCustomMapping
    {
        public int Evaluation { get; set; }

        public string Title { get; set; }

        public string AdditionalThoughts { get; set; }

        public string GuestId { get; set; }

        public string GuestName { get; set; }

        public DateTime SubmitDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<GuestReview, DoneGuestReviewServiceModel>()
                .ForMember(dhr => dhr.GuestId, cfg => cfg.MapFrom(r => r.EvaluatedGuestId))
                .ForMember(dhr => dhr.GuestName, cfg => cfg.MapFrom(r => r.EvaluatedGuest.UserName));
        }
    }
}
