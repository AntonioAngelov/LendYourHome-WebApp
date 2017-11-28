namespace LendYourHome.Services.ServiceModels.Users
{
    using System.ComponentModel.DataAnnotations;
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

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsServiceModel>()
                .ForMember(uds => uds.Ishost, cfg => cfg.MapFrom(u => u.Home != null));
        }
    }
}
