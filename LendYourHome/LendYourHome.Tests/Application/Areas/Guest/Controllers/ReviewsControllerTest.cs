namespace LendYourHome.Tests.Application.Areas.Guest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using FluentAssertions;
    using LendYourHome.Application.Areas.Guest.Controllers;
    using LendYourHome.Application.Areas.Guest.Models.Reviews;
    using LendYourHome.Services.ServiceModels.Homes;
    using LendYourHome.Services.ServiceModels.Reviews;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Mocks.Services;
    using Moq;
    using Xunit;

    public class ReviewsControllerTest
    {
        [Fact]
        public void Waiting_ShouldReturnViewResultWithAllWaitingReviewsForCurrentUser()
        {
            //Arrange
            var userId = "id";
            var expectedPicUrl = "pic";
            var expectedHomes = new List<HomeForReviewServiceModel>
            {
                new HomeForReviewServiceModel
                {
                    Id = 1,
                    OwnerId = "ownerId",
                    OwnerName = "Name",
                    OwnerPictureUrl = expectedPicUrl
                }
            };


            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.WaitingForReview(It.Is<string>(id => id == userId)))
                .Returns(expectedHomes);

            var pictureService = PictureServiceMock.New;
            pictureService
                .Setup(ps => ps.PreparePictureToDisplay(It.IsAny<string>()))
                .Returns(expectedPicUrl);

            var controller = new ReviewsController(null, homeService.Object, userManager.Object, pictureService.Object);

            //Act
            var result = controller.Waiting();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<IEnumerable<HomeForReviewServiceModel>>().Equals(expectedHomes));
        }

        [Fact]
        public void Done_ShouldReturnViewResultWithAllDoneReviewsForCurrentUser()
        {
            //Arrange
            var userId = "id";
            var expectedReviews = new List<DoneHomeReviewServiceModel>
            {
                new DoneHomeReviewServiceModel
                {
                    Evaluation = 5,
                    AdditionalThoughts = "good",
                    HomeId = 1,
                    OwnerId = "ownerId",
                    OwnerName = "owner",
                    SubmitDate = DateTime.UtcNow,
                    Title = "good stay"

                }
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);

            var homeReviewsService = HomeReviewServiceMock.New;
            homeReviewsService
                .Setup(hrs => hrs.Done(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(expectedReviews);

            var controller = new ReviewsController(homeReviewsService.Object, null, userManager.Object, null);

            //Act
            var result = controller.Done(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<DoneHomeReviewsViewModel>().Reviews.Equals(expectedReviews));
        }
    }
}
