namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Homes;

    public interface IHomeService
    {
        bool Exists(string ownerId);

        bool Exists(int homeId);

        HomeDetailsServiceModel Find(int homeId);

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
            (string country,
            string city,
            int minBedrooms,
            int maxBedrooms,
            int minBathrooms,
            int maxBathrooms,
            int minSleeps,
            int maxSleeps,
            decimal minPrice,
            decimal maxPrice);
    }
}