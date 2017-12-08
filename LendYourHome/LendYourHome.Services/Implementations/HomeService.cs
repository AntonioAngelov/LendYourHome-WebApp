namespace LendYourHome.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using ServiceModels.Homes;

    internal class HomeService : IHomeService
    {
        private readonly LendYourHomeDbContext db;

        public HomeService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public bool Exists(string ownerId)
            => this.db.Homes
                .Any(h => h.OwnerId == ownerId);

        public bool Exists(int homeId)
            => this.db.Homes
            .Any(h => h.Id == homeId);

        public HomeDetailsServiceModel Find(int homeId)
            => this.db.Homes
                .Where(h => h.Id == homeId)
                .ProjectTo<HomeDetailsServiceModel>()
                .FirstOrDefault();

        public void Create(string country,
            string city,
            string address,
            int sleeps,
            int bedrooms,
            int bathrooms,
            decimal pricePerNight,
            string additionalInfo,
            bool isActiveOffer,
            List<string> picturesPaths,
            string ownerId)
        {
            var home = new Home
            {
                Country = country,
                City = city,
                Additionalnformation = additionalInfo,
                Sleeps = sleeps,
                Address = address,
                Bathrooms = bathrooms,
                Bedrooms = bedrooms,
                IsActiveOffer = isActiveOffer,
                PricePerNight = pricePerNight,
                Pictures = picturesPaths.Select(p => new Picture
                {
                    Url = p
                })
                .ToList(),
                OwnerId = ownerId
            };

            this.db.Add(home);

            this.db.SaveChanges();
        }

        public IEnumerable<HomeOfferServiceModel> All(string country,
            string city,
            int minBedrooms,
            int maxBedrooms,
            int minBathrooms,
            int maxBathrooms,
            int minSleeps,
            int maxSleeps,
            decimal minPrice,
            decimal maxPrice)
        {
            var homes = this.db
                .Homes
                .Where(h =>
                    h.IsActiveOffer &&
                    h.Bathrooms >= minBathrooms &&
                    h.Bathrooms <= maxBathrooms &&
                    h.Bedrooms >= minBedrooms &&
                    h.Bedrooms <= maxBedrooms &&
                    h.Sleeps >= minSleeps &&
                    h.Sleeps <= maxSleeps &&
                    h.PricePerNight <= maxPrice &&
                    h.PricePerNight >= minPrice)
                .ProjectTo<HomeOfferServiceModel>()
                .OrderByDescending(h => h.AverageRating)
                .ThenBy(h => h.PricePerNight)
                .ToList();

            if (country != null)
            {
                homes = homes
                    .Where(h => h.Country.ToLower() == country.ToLower())
                    .ToList();
            }

            if (city != null)
            {
                homes = homes
                    .Where(h => h.City.ToLower() == city.ToLower())
                    .ToList();
            }

            return homes;
        }
    }
}
