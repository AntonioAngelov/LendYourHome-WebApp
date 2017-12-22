namespace LendYourHome.Application.Areas.Host.Models.Bookings
{
    using System.Collections.Generic;
    using Application.Models;
    using Services.ServiceModels.Bookings;

    public class HostBookingsViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<HostBookingsServiceModel> Bookings { get; set; }
    }
}
