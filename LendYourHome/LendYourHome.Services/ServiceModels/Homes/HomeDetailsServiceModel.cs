namespace LendYourHome.Services.ServiceModels.Homes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class HomeDetailsServiceModel : IMapFrom<Home>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }
        
        public int Sleeps { get; set; }

        public int Bedrooms { get; set; }
        
        public int Bathrooms { get; set; }
        
        [Display(Name = "Price per Night")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Additional Info")]
        public string Additionalnformation { get; set; }

        public List<string> HomePicturesUrls { get; set; }

        public string OwnerId { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPictureUrl { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Home, HomeDetailsServiceModel>()
                .ForMember(hd => hd.OwnerName, cfg => cfg.MapFrom(h => h.Owner.UserName))
                .ForMember(hd => hd.OwnerPictureUrl, cfg => cfg.MapFrom(h => h.Owner.ProfilePictureUrl))
                .ForMember(hd => hd.HomePicturesUrls, cfg => cfg.MapFrom(h => h.Pictures.Select(p => p.Url).ToList()));
        }
    }
}
