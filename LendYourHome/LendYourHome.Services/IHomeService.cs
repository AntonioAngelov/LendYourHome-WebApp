﻿namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using System.Data;
    using ServiceModels.Homes;

    public interface IHomeService
    {
        bool Exists(string ownerId);

        bool Exists(int homeId);

        int GetId(string ownerId);

        HomeDetailsServiceModel Find(int homeId);

        PersonalHomeDetailsServiceModel Find(string ownerId);

        IEnumerable<HomeOfferServiceModel> TopSixByAverageRating();

        void Edit(
            string ownerId,
            int sleeps,
            string country,
            string city,
            string additionalnformation,
            int bathrooms,
            int bedrooms,
            bool isActiveOffer,
            string address,
            decimal pricePerNight);

        void Create(
            string country,
            string city,
            string address,
            int sleeps,
            int bedrooms,
            int bathrooms,
            decimal pricePerNight,
            string additionalInfo,
            bool isActiveOffer,
            List<string> picturesPaths,
            string ownerId);

        IEnumerable<HomeOfferServiceModel> All
            (int pageNumber, 
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
            decimal maxPrice);

        int Total(string country,
            string city,
            int minBedrooms,
            int maxBedrooms,
            int minBathrooms,
            int maxBathrooms,
            int minSleeps,
            int maxSleeps,
            decimal minPrice,
            decimal maxPrice);

        IEnumerable<HomeForReviewServiceModel> WaitingForReview(string guestId);

        string GetOwnerName(int homeId);
    }
}