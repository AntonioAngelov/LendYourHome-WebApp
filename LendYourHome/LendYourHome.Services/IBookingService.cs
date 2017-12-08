namespace LendYourHome.Services
{
    using System;
    using System.Collections.Generic;
    using ServiceModels.Bookings;

    public interface IBookingService
    {
        bool Exists(int bookingId,string hostId);

        void Approve(int bookingId);

        void Create(string guestId, int homeId, DateTime checkInDate, DateTime checkOutDate);

        IEnumerable<GuestBookingServiceModel> GuestBookings(string guestId, bool approved);

        IEnumerable<HostBookingsServiceModel> HostBookings(string hostId, bool approved);
    }
}
