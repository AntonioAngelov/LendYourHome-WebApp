namespace LendYourHome.Common.Constants
{
    public class ApplicationConstants
    {
        // roles
        public const string AdminRole = "Administrator";
        public const string HostRole = "Host";

        //temp data constants
        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";
        public const string TempDataHomeIdKey = "HomeId";
        public const string TempDataHomeOwnerNameKey = "OwnerName";
        public const string TempDataGuestNameKey = "GuestName";

        //areas
        public const string GuestArea = "Guest";
        public const string HostArea = "Host";
        public const string AdminArea = "Admin";

        //policy
        public const string HostEntryPolicy = "HostEntry";

        //view bag keys
        public const string ViewDataHomeOffersKey = "HomeOffers";

        //pagination constants 
        public const int HomeOffersPageListingSize = 9;
        public const int DoneReviewsPageListingSize = 4;
        public const int ReviewsPageListinSize = 5;
    }
}
