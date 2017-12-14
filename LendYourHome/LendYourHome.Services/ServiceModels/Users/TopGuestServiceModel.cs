namespace LendYourHome.Services.ServiceModels.Users
{
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class TopGuestServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string UserName { get; set; }

        public int TotalRating { get; set; }

        public int TotalReviews { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, TopGuestServiceModel>()
                .ForMember(uds => uds.TotalRating, cfg => cfg.MapFrom(u => u.GuestReviewsReceived.Any() ? u.GuestReviewsReceived.Sum(r => r.Evaluation) : 0))
                .ForMember(uds => uds.TotalReviews, cfg => cfg.MapFrom(u => u.GuestReviewsReceived.Count));
        }
    }
}
