namespace LendYourHome.Services.ServiceModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class UserDetailsServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        public string Address { get; set; }

        [Display(Name = "Additional Info")]
        public string AdditionalInformation { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool Ishost { get; set; }

        public int HomeId { get; set; }

        public int TotalRating { get; set; }

        public int TotalReviews { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsServiceModel>()
                .ForMember(uds => uds.Ishost, cfg => cfg.MapFrom(u => u.Home != null))
                .ForMember(uds => uds.HomeId, cfg => cfg.MapFrom(u => u.Home != null ? u.Home.Id : 0))
                .ForMember(uds => uds.TotalRating, cfg => cfg.MapFrom(u =>u.GuestReviewsReceived.Any() ? u.GuestReviewsReceived.Sum(r => r.Evaluation) : 0))
                .ForMember(uds => uds.TotalReviews, cfg => cfg.MapFrom(u => u.GuestReviewsReceived.Count));
        }
    }
}
