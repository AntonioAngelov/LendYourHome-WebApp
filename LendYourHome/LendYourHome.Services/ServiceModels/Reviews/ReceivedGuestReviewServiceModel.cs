namespace LendYourHome.Services.ServiceModels.Reviews
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class ReceivedGuestReviewServiceModel : IMapFrom<GuestReview>, IHaveCustomMapping
    {
        public int Evaluation { get; set; }

        public string Title { get; set; }

        public string AdditionalThoughts { get; set; }

        public DateTime SubmitDate { get; set; }

        public string HostId { get; set; }

        public string HostProfilePictureUrl { get; set; }

        public string HostName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<GuestReview, ReceivedGuestReviewServiceModel>()
                .ForMember(rgr => rgr.HostProfilePictureUrl, cfg => cfg.MapFrom(r => r.Host.ProfilePictureUrl))
                .ForMember(rgr => rgr.HostName, cfg => cfg.MapFrom(r => r.Host.UserName));
        }
    }
}
