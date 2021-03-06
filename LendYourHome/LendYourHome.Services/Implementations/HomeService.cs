﻿namespace LendYourHome.Services.Implementations
{
    using System;
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

        public int GetId(string ownerId)
            => this.db.Homes
                .FirstOrDefault(h => h.OwnerId == ownerId)
                .Id;

        public HomeDetailsServiceModel Find(int homeId)
            => this.db.Homes
                .Where(h => h.Id == homeId)
                .ProjectTo<HomeDetailsServiceModel>()
                .FirstOrDefault();

        public PersonalHomeDetailsServiceModel Find(string ownerId)
            => this.db.Homes
                .Where(h => h.OwnerId == ownerId)
                .ProjectTo<PersonalHomeDetailsServiceModel>()
                .FirstOrDefault();

        public void Edit(string ownerId, 
            int sleeps, 
            string country, 
            string city, 
            string additionalnformation, 
            int bathrooms,
            int bedrooms, 
            bool isActiveOffer,
            string address,
            decimal pricePerNight)
        {
            var home = this.db.Homes
                .Where(h => h.OwnerId == ownerId)
                .FirstOrDefault();

            home.Sleeps = sleeps;
            home.Country = country;
            home.City = city;
            home.Additionalnformation = additionalnformation;
            home.Bathrooms = bathrooms;
            home.Bedrooms = bedrooms;
            home.IsActiveOffer = isActiveOffer;
            home.Address = address;
            home.PricePerNight = pricePerNight;

            this.db.SaveChanges();
        }

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

        public IEnumerable<HomeOfferServiceModel> TopSixByAverageRating()
            => this.db.Homes
                .Where(h => h.IsActiveOffer)
                .OrderByDescending(h =>  h.Reviews.Sum(r => r.Evaluation) / (double)h.Reviews.Count)
                .Take(6)
                .ProjectTo<HomeOfferServiceModel>()
                .ToList();

        public IEnumerable<HomeOfferServiceModel> All(int pageNumber, 
            int pageSize, 
            string country,
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
                .ToList();

            if (!string.IsNullOrEmpty(country))
            {
                homes = homes
                    .Where(h => h.Country.ToLower().Contains(country.ToLower().Trim()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                homes = homes
                    .Where(h => h.City.ToLower().Contains(city.ToLower().Trim()))
                    .ToList();
            }

            return homes
                .OrderByDescending(h => h.TotalRating / (h.TotalReviewsCount != 0 ? (double)h.TotalReviewsCount : 1))
                .ThenBy(h => h.PricePerNight)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int Total(string country,
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
                    .AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                homes = homes
                    .Where(h => h.Country.ToLower().Contains(country.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(city))
            {
                homes = homes
                    .Where(h => h.City.ToLower().Contains(city.ToLower().Trim()));
            }

            return homes.Count();
        }

        public IEnumerable<HomeForReviewServiceModel> WaitingForReview(string guestId)
            => this.db.Homes
                .Where(h => h.Bookings.Count(b => b.GuestId == guestId && 
                                                    b.IsApproved &&
                                                    b.CheckOutDate <= DateTime.UtcNow)
                            > h.Reviews.Count(r => r.EvaluatingGuestId == guestId))
                .ProjectTo<HomeForReviewServiceModel>()
                .ToList();

        public string GetOwnerName(int homeId)
            => this.db.Homes
                .Where(h => h.Id == homeId)
                .Select(h => h.Owner.UserName)
                .FirstOrDefault();
    }
}
