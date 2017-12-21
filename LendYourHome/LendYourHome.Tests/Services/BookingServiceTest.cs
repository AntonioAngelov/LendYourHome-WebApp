namespace LendYourHome.Tests.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using FluentAssertions;
    using LendYourHome.Services.Implementations;
    using Mocks;
    using Xunit;

    public class BookingServiceTest
    {
        private const string guestId = "guestId";
        private const string hostID = "hostId";

        public BookingServiceTest()
        {
            try
            {
                Tests.Initialize();
            }
            catch (Exception e)
            {
            }
        }

        [Fact]
        public async Task Exists_ShouldReturnTrueForExistingBooking()
        {
            //Arrange
            var homeId = 1;
            var bookingId = 1;

            var booking = new Booking
            {
                Id = bookingId,
                GuestId = guestId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11)
            };

            booking.Home = new Home
            {
                Id = homeId,
                OwnerId = hostID,
                Additionalnformation = "info",
                Address = "address",
                Bathrooms = 1,
                Bedrooms = 1,
                City = "Sofia",
                Country = "Bulgaria",
                IsActiveOffer = true,
                Sleeps = 1,
                PricePerNight = 10
            };

            var db = LendYourHomeDbMock.New();
            db.Bookings.Add(booking);
            await db.SaveChangesAsync();

            var service = new BookingService(db);

            //Act
            var result = service.Exists(bookingId, hostID);

            //Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Exists_ShouldReturnFalseForNonExistingBoking()
        {
            //Arrange 
            var db = LendYourHomeDbMock.New();
            var service = new BookingService(db);

            //Act
            var result = service.Exists(1, "a");

            //Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task Approve_ShouldApproveBooking()
        {
            //Arrange
            var bookingId = 1;

            var db = LendYourHomeDbMock.New();
            db.Bookings.Add(new Booking
            {
                Id = bookingId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11),
                IsApproved = false
            });
            await db.SaveChangesAsync();

            var service = new BookingService(db);

            //Act
            service.Approve(bookingId);

            //Assert
            db.Bookings
                .Any(b => b.Id == bookingId && b.IsApproved == true)
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Create_ShouldAddNewBookingWithTeGivenParamsIntoTheDb()
        {
            //Arrange
            var homeId = 1;
            var checkInDate = DateTime.UtcNow.AddDays(10);
            var checkOutDate = DateTime.UtcNow.AddDays(11);

            var db = LendYourHomeDbMock.New();
            var service = new BookingService(db);

            //Act
            service.Create(guestId, homeId, checkInDate, checkOutDate);

            //Assert
            db.Bookings
                .Any(b => b.GuestId == guestId
                          && b.HomeId == homeId
                          && b.CheckInDate == checkInDate
                          && b.CheckOutDate == checkOutDate)
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task GuestBookings_WithApprovedSetToTrue_ShouldReturnOnlyApprovedBookingsForGivenGuestId()
        {
            //Arrange
            var db = LendYourHomeDbMock.New();
            await this.SeedBookingsAsync(db);

            var service = new BookingService(db);

            //Act
            var result = service.GuestBookings(guestId, true);

            //Assert
            result
                .Count()
                .Should()
                .Be(1);
        }

        [Fact]
        public async Task GuestBookings_WithApprovedSetToFalse_ShouldReturnOnlyPendngBookingsForGivenGuestId()
        {
            //Arrange
            var db = LendYourHomeDbMock.New();
            await this.SeedBookingsAsync(db);

            var service = new BookingService(db);

            //Act
            var result = service.GuestBookings(guestId, false);

            //Assert
            result
                .Count()
                .Should()
                .Be(3);
        }

        [Fact]
        public async Task HostBookings_WithApprovedSetToTrue_ShouldReturnOnlyApprovedBookingsForGivenHosttId()
        {
            //Arrange
            var db = LendYourHomeDbMock.New();
            await this.SeedBookingsAsync(db);

            var service = new BookingService(db);

            //Act
            var result = service.HostBookings(hostID, true);

            //Assert
            result
                .Count()
                .Should()
                .Be(1);
        }

        [Fact]
        public async Task HostBookings_WithApprovedSetToFalse_ShouldReturnOnlyPendingBookingsForGivenHosttId()
        {
            //Arrange
            var db = LendYourHomeDbMock.New();
            await this.SeedBookingsAsync(db);

            var service = new BookingService(db);

            //Act
            var result = service.HostBookings(hostID, false);

            //Assert
            result
                .Count()
                .Should()
                .Be(3);
        }

        private async Task SeedBookingsAsync(LendYourHomeDbContext db)
        {
            var bookingId = 0;

            var home = new Home
            {
                Id = 1,
                OwnerId = hostID,
                Additionalnformation = "info",
                Address = "address",
                Bathrooms = 1,
                Bedrooms = 1,
                City = "Sofia",
                Country = "Bulgaria",
                IsActiveOffer = true,
                Sleeps = 1,
                PricePerNight = 10
            };

            var booking1 = new Booking
            {
                Id = ++bookingId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11),
                IsApproved = false,
                GuestId = guestId,
                Home = home
            };

            var booking2 = new Booking
            {
                Id = ++bookingId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11),
                IsApproved = false,
                GuestId = guestId,
                Home = home
            };

            var booking3 = new Booking
            {
                Id = ++bookingId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11),
                IsApproved = true,
                GuestId = guestId,
                Home = home
            };

            var booking4 = new Booking
            {
                Id = ++bookingId,
                CheckInDate = DateTime.UtcNow.AddDays(10),
                CheckOutDate = DateTime.UtcNow.AddDays(11),
                IsApproved = false,
                GuestId = guestId,
                Home = home
            };

            db.Bookings.Add(booking1);
            db.Bookings.Add(booking2);
            db.Bookings.Add(booking3);
            db.Bookings.Add(booking4);
            await db.SaveChangesAsync();
        }
    }
}
