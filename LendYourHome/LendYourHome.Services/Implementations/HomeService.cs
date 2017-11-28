namespace LendYourHome.Services.Implementations
{
    using System.Linq;
    using Data;
    using Data.Models;

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

        public void Create(string country,
            string city, 
            string address, 
            int sleeps, 
            int bedrooms, 
            int bathrooms, 
            decimal pricePerNight,
            string additionalInfo, 
            bool isActiveOffer,
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
                OwnerId = ownerId
            };

            this.db.Add(home);

            this.db.SaveChanges();
        }
    }
}
