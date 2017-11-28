namespace LendYourHome.Services
{
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
            string ownerId);
    }
}