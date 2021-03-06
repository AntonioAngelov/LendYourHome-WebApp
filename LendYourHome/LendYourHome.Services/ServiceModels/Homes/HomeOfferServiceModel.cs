﻿namespace LendYourHome.Services.ServiceModels.Homes
{
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class HomeOfferServiceModel : IMapFrom<Home>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Country { get; set; }
        
        public string City { get; set; }

        public string PictureUrl { get; set; }
       
        public int Sleeps { get; set; }

        public int Bedrooms { get; set; }
        
        public int Bathrooms { get; set; }

        public decimal PricePerNight { get; set; }

        public int TotalRating { get; set; }

        public int TotalReviewsCount { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Home, HomeOfferServiceModel>()
                .ForMember(ho => ho.PictureUrl,
                    cfg => cfg.MapFrom(h => h.Pictures
                        .OrderBy(p => p.Url)
                        .Select(p => p.Url)
                        .FirstOrDefault()))
                .ForMember(ho => ho.TotalRating,
                    cfg => cfg.MapFrom(h =>
                        h.Reviews.Any() ? h.Reviews.Sum(r => r.Evaluation) : 0))
                .ForMember(ho => ho.TotalReviewsCount, cfg => cfg.MapFrom(h => h.Reviews.Count));
        }
    }
}
