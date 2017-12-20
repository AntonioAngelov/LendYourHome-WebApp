namespace LendYourHome.Tests.Application.Controllers
{
    using System.Security.Claims;
    using FluentAssertions;
    using LendYourHome.Application.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Mocks.Services;
    using Moq;
    using Xunit;

    public class HomesControllerTest
    {
        [Fact]
        public void Create_ForUserThatIsAlreadyHost_MustReturnRedirectToActionResult()
        {
            //Arrange
            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("someId");

            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.Exists(It.IsAny<string>()))
                .Returns(true);

            var controller = new HomesController(homeService.Object, null, userManager.Object, null, null);

            //Act
            var result = controller.Create();

            //Assert 
            result
                .Should()
                .BeOfType<RedirectToActionResult>()
                .Subject
                .ActionName
                .Should()
                .Be("Index");
        }

        [Fact]
        public void Details_WithInvaidId_ShouldReturnNotFoundResult()
        {
            //Arange
            var homeService = HomeServiceMock.New;
            homeService
                .Setup(hs => hs.Exists(It.IsAny<int>()))
                .Returns(false);

            var controller = new HomesController(homeService.Object, null, null, null, null);

            //Act
            var result = controller.Details(It.IsAny<int>());

            //Asset
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }
    }
}
