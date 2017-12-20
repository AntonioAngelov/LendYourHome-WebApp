namespace LendYourHome.Tests.Application.Controllers
{
    using System.Collections.Generic;
    using FluentAssertions;
    using LendYourHome.Application.Controllers;
    using LendYourHome.Application.Models;
    using Microsoft.AspNetCore.Mvc;
    using Mocks.Services;
    using Moq;
    using Services.ServiceModels.Homes;
    using Services.ServiceModels.Users;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void Index_ShouldReturnValidViewWithCorrectData()
        {
            //Arange
            var pictureUrl = "pic";

            var expectedTopHomes = new List<HomeOfferServiceModel>
            {
                new HomeOfferServiceModel
                {
                    Id = 1,
                    Bathrooms = 1,
                    Bedrooms = 1,
                    City = "Sofia",
                    Country = "Bulgaria",
                    PricePerNight = 10,
                    Sleeps = 1,
                    TotalRating = 10,
                    TotalReviewsCount = 2,
                    PictureUrl = pictureUrl
                },
                new HomeOfferServiceModel
                {
                    Id = 1,
                    Bathrooms = 1,
                    Bedrooms = 1,
                    City = "Amsterdam",
                    Country = "Holland",
                    PricePerNight = 10,
                    Sleeps = 1,
                    TotalRating = 15,
                    TotalReviewsCount = 2,
                    PictureUrl = pictureUrl
                }
            };

            var topGuests = new List<TopGuestServiceModel>
            {
                new TopGuestServiceModel
                {
                    Id = "someId",
                    TotalReviews = 2,
                    TotalRating = 10,
                    UserName = "Someone",
                    ProfilePictureUrl = pictureUrl
                },
                new TopGuestServiceModel
                {
                    Id = "someId1",
                    TotalReviews = 2,
                    TotalRating = 15,
                    UserName = "Someone1",
                    ProfilePictureUrl = pictureUrl
                }
            };

            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.TopSixByAverageRating())
                .Returns(expectedTopHomes);

            var userService = UserServiceMock.New;
            userService
                .Setup(us => us.TopSixGuestsByAverageRating())
                .Returns(topGuests);

            var pictureService = PictureServiceMock.New;
            pictureService
                .Setup(ps => ps.PreparePictureToDisplay(It.IsAny<string>()))
                .Returns(pictureUrl);

            var controller = new HomeController(homeService.Object, userService.Object, pictureService.Object);

            //Act
            var result = controller.Index();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<HomeIndexViewModel>().TopGuests.Equals(topGuests) 
                            && m.As<HomeIndexViewModel>().TopHomes.Equals(expectedTopHomes));
        }
    }
}
