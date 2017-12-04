namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Homes;

    public interface IHomeService
    {
        bool Exists(string ownerId);

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
            int bedrooms,
            int bathrooms,
            int sleeps, 
            decimal minPrice,
            decimal maxPrice);
    }
}