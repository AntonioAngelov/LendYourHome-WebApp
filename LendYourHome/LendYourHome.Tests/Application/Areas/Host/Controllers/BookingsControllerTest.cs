namespace LendYourHome.Tests.Application.Areas.Host.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using FluentAssertions;
    using LendYourHome.Application.Areas.Host.Controllers;
    using LendYourHome.Application.Areas.Host.Models.Bookings;
    using LendYourHome.Services.ServiceModels.Bookings;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Mocks.Services;
    using Moq;
    using Xunit;

    public class BookingsControllerTest
    {
        [Fact]
        public void Pending_ShouldReturnViewResultWithAllPendingBookingsForCurrentHost()
        {
            //Arrange
            var userId = "id";
            var expectedPicUrl = "pic";
            var expectedBookings = new List<HostBookingsServiceModel>
            {
                new HostBookingsServiceModel
                {
                    CheckInDate = DateTime.UtcNow.AddDays(10),
                    CheckOutDate = DateTime.UtcNow.AddDays(11),
                    GuestId = "guest",
                    GuestProfilePictureUrl = expectedPicUrl,
                    GuestUsername = "Pesho",
                    Id = 1,
                    TotalPrice = 10
                }
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var pictureService = PictureServiceMock.New;
            pictureService
                .Setup(ps => ps.PreparePictureToDisplay(It.IsAny<string>()))
                .Returns(expectedPicUrl);

            var bookingService = BookingServiceMock.New;
            bookingService
                .Setup(bs => bs.HostBookings(It.IsAny<int>(), It.IsAny<int>(), It.Is<string>(id => id == userId), false))
                .Returns(expectedBookings);

            var controller = new BookingsController(bookingService.Object, userManager.Object, pictureService.Object);

            //Act
            var result = controller.Pending();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<HostBookingsViewModel>().Bookings.Equals(expectedBookings));
        }

        [Fact]
        public void Approved_ShouldReturnViewResultWithAllApprovedBookingsForCurrentHost()
        {
            //Arrange
            var userId = "id";
            var expectedPicUrl = "pic";
            var expectedBookings = new List<HostBookingsServiceModel>
            {
                new HostBookingsServiceModel
                {
                    CheckInDate = DateTime.UtcNow.AddDays(10),
                    CheckOutDate = DateTime.UtcNow.AddDays(11),
                    GuestId = "guest",
                    GuestProfilePictureUrl = expectedPicUrl,
                    GuestUsername = "Pesho",
                    Id = 1,
                    TotalPrice = 10
                }
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var pictureService = PictureServiceMock.New;
            pictureService
                .Setup(ps => ps.PreparePictureToDisplay(It.IsAny<string>()))
                .Returns(expectedPicUrl);

            var bookingService = BookingServiceMock.New;
            bookingService
                .Setup(bs => bs.HostBookings(It.IsAny<int>(), It.IsAny<int>(), It.Is<string>(id => id == userId), true))
                .Returns(expectedBookings);

            var controller = new BookingsController(bookingService.Object, userManager.Object, pictureService.Object);

            //Act
            var result = controller.Approved();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<HostBookingsViewModel>().Bookings.Equals(expectedBookings));
        }
    }
}
