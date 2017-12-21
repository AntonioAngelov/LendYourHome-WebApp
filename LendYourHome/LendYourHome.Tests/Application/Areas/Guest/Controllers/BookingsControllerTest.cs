namespace LendYourHome.Tests.Application.Areas.Guest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using FluentAssertions;
    using LendYourHome.Application.Areas.Guest.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Mocks;
    using Mocks.Services;
    using Moq;
    using Xunit;
    using LendYourHome.Services.ServiceModels.Bookings;

    public class BookingsControllerTest
    {
        [Fact]
        public void Create_WithInvalidHomeId_ShouldReturnNotFoundReult()
        {
            //Arrange
            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.Exists(It.IsAny<int>()))
                .Returns(false);

            var controller = new BookingsController(null, homeService.Object, null);

            //Act
            var result = controller.Create(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Create_WithValidHomeId_ShouldReturnViewResult()
        {
            //Arrange
            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.Exists(It.IsAny<int>()))
                .Returns(true);

            var controller = new BookingsController(null, homeService.Object, null);

            var tempDataMock = new Mock<TempDataDictionary>(new Mock<HttpContext>().Object, new Mock<ITempDataProvider>().Object);
            controller.TempData = tempDataMock.Object;

            //Act
            var result = controller.Create(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void Pending_ShouldReturnAllPendingBookingsForCurrentUser()
        {
            //Arrange 
            var userId = "id";
            var expectedBookings = new List<GuestBookingServiceModel>
            {
                new GuestBookingServiceModel
                {
                    CheckInDate = DateTime.MinValue,
                    CheckOutDate = DateTime.MaxValue,
                    GuestId = userId,
                    GuestUsername = "Pesho",
                    HomeId = 1,
                    OwnerUsername = "Gosho",
                    PricePerNight = 10,
                    TotalPrice = 10000000
                }
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var bookingService = BookingServiceMock.New;
            bookingService
                .Setup(bs => bs.GuestBookings(It.Is<string>(id => id == userId), false))
                .Returns(expectedBookings);

            var controller = new BookingsController(bookingService.Object, null, userManager.Object);

            //Act 
            var result = controller.Pending();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<IEnumerable<GuestBookingServiceModel>>().Equals(expectedBookings));
        }

        [Fact]
        public void Approved_ShouldReturnAllApprovedBookingsForCurrentUser()
        {
            //Arrange 
            var userId = "id";
            var expectedBookings = new List<GuestBookingServiceModel>
            {
                new GuestBookingServiceModel
                {
                    CheckInDate = DateTime.MinValue,
                    CheckOutDate = DateTime.MaxValue,
                    GuestId = userId,
                    GuestUsername = "Pesho",
                    HomeId = 1,
                    OwnerUsername = "Gosho",
                    PricePerNight = 10,
                    TotalPrice = 10000000
                }
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var bookingService = BookingServiceMock.New;
            bookingService
                .Setup(bs => bs.GuestBookings(It.Is<string>(id => id == userId), true))
                .Returns(expectedBookings);

            var controller = new BookingsController(bookingService.Object, null, userManager.Object);

            //Act 
            var result = controller.Approved();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<IEnumerable<GuestBookingServiceModel>>().Equals(expectedBookings));
        }
    }
}
