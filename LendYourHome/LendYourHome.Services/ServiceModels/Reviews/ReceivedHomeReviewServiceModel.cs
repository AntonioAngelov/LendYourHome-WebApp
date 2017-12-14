namespace LendYourHome.Services.ServiceModels.Reviews
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ReceivedHomeReviewServiceModel : IMapFrom<HomeReview>, IHaveCustomMapping
    {
        public int Evaluation { get; set; }

        public string Title { get; set; }

        public string AdditionalThoughts { get; set; }

        public DateTime SubmitDate { get; set; }

        public string EvaluatingGuestId { get; set; }

        public string GuestName { get; set; }

        public string GuestProfilePictureUrl { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<HomeReview, ReceivedHomeReviewServiceModel>()
                .ForMember(rhr => rhr.GuestName, cfg => cfg.MapFrom(r => r.EvaluatingGuest.UserName))
                .ForMember(rhr => rhr.GuestProfilePictureUrl,
                    cfg => cfg.MapFrom(r => r.EvaluatingGuest.ProfilePictureUrl));
        }
    }
}
