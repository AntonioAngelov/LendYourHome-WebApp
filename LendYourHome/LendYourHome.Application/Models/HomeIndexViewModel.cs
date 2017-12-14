namespace LendYourHome.Application.Models
{
    using System.Collections.Generic;
    using Services.ServiceModels.Homes;
    using Services.ServiceModels.Users;

    public class HomeIndexViewModel
    {
        public IEnumerable<HomeOfferServiceModel> TopHomes { get; set; }

        public IEnumerable<TopGuestServiceModel> TopGuests { get; set; }
    }
}
