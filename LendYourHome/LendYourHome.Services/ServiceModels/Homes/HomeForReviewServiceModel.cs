namespace LendYourHome.Services.ServiceModels.Homes
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    public class HomeForReviewServiceModel : IMapFrom<Home>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPictureUrl { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Home, HomeForReviewServiceModel>()
                .ForMember(hd => hd.OwnerName, cfg => cfg.MapFrom(h => h.Owner.UserName))
                .ForMember(hd => hd.OwnerPictureUrl, cfg => cfg.MapFrom(h => h.Owner.ProfilePictureUrl));
        }
    }
}
