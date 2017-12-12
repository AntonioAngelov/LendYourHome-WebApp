namespace LendYourHome.Services.ServiceModels.Reviews
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class DoneHomeReviewServiceModel : IMapFrom<HomeReview>, IHaveCustomMapping
    {
        public int Evaluation { get; set; }
        
        public string Title { get; set; }
        
        public string AdditionalThoughts { get; set; }

        public int HomeId { get; set; }

        public string OwnerId { get; set; }

        public string OwnerName { get; set; }

        public DateTime SubmitDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<HomeReview, DoneHomeReviewServiceModel>()
                .ForMember(dhr => dhr.OwnerId, cfg => cfg.MapFrom(r => r.Home.OwnerId))
                .ForMember(dhr => dhr.OwnerName, cfg => cfg.MapFrom(r => r.Home.Owner.UserName));
        }
    }
}
