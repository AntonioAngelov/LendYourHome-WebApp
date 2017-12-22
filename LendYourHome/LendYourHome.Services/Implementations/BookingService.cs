namespace LendYourHome.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using ServiceModels.Bookings;

    public class BookingService : IBookingService
    {
        private readonly LendYourHomeDbContext db;

        public BookingService(LendYourHomeDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int bookingId, string hostId)
            => this.db.Bookings
                .Any(b => b.Home.OwnerId == hostId && b.Id == bookingId);

        public void Approve(int bookingId)
        {
            var booking = this.db.Bookings
                .Find(bookingId);

            if (booking != null)
            {
                booking.IsApproved = true;
            }

            this.db.SaveChanges();
        }

        public void Create(string guestId, int homeId, DateTime checkInDate, DateTime checkOutDate)
        {
            var booking = new Booking
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                HomeId = homeId,
                GuestId = guestId,
                IsApproved = false
            };

            this.db.Bookings.Add(booking);
            this.db.SaveChanges();
        }

        public IEnumerable<GuestBookingServiceModel> GuestBookings(string guestId, bool approved = false)
            => this.db
                .Bookings
                .Where(b => b.IsApproved  == approved && b.GuestId == guestId && b.CheckInDate > DateTime.UtcNow)
                .OrderBy(b => b.CheckInDate)
                .ProjectTo<GuestBookingServiceModel>()
                .ToList();

        public IEnumerable<HostBookingsServiceModel> HostBookings(int pageNumber,
            int pageSize, string hostId, bool approved = false)
            => this.db
                .Bookings
                .Where(b => b.IsApproved == approved && b.Home.OwnerId == hostId && b.CheckInDate > DateTime.UtcNow)
                .OrderByDescending(b => b.CheckInDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<HostBookingsServiceModel>()
                .ToList();

        public int TotalBookingsByHost(string hostId, bool approved)
            => this.db
                .Bookings
                .Count(b => b.IsApproved == approved && b.Home.OwnerId == hostId && b.CheckInDate > DateTime.UtcNow);
    }
}
