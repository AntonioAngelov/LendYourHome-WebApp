namespace LendYourHome.Tests.Application.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using LendYourHome.Application.Controllers;
    using Xunit;
    using FluentAssertions;
    using LendYourHome.Application.Models.UsersViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Mocks.Services;
    using Moq;
    using Services.ServiceModels.Users;

    public class UsersControllerTest
    {
        public UsersControllerTest()
        {
            Setup.Initialize();
        }

        [Fact]
        public void Edit_ShouldBeOnlyForAuthorizedUsers()
        {
            //Arrange
            var method = typeof(UsersController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(UsersController.Edit) && !m.GetParameters().Any());

            //Act
            var attributes = method.GetCustomAttributes(true);

            //Assert
            attributes
                .Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public void Edit_ShouldReturnValidViewWithCorrectDataForCurrentUser()
        {
            //Arrange
            var userIdValue = "userId";
            var pictureUrl = "picture";

            var expectedUser = new UserEditServiceModel
            {
                UserName = "test",
                Address = "test",
                AdditionalInformation = "testing here"
            };

            var userManager = UserManagerMock.New;
            userManager
                .Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userIdValue);

            var userService = UserServiceMock.New;
            userService
                .Setup(us => us.GetForEdit(It.Is<string>(userId => userId == userIdValue)))
                .Returns(expectedUser);

            var pictureService = PictureServiceMock.New;
            pictureService
                .Setup(ps => ps.PreparePictureToDisplay(It.IsAny<string>()))
                .Returns(pictureUrl);

            var controller = new UsersController(userService.Object, null, pictureService.Object, userManager.Object);
            

            //Act 
            var result = controller.Edit();

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserEditViewModel>().FormDataModel == expectedUser
                            && m.As<UserEditViewModel>().ProfilePictureUrl == pictureUrl);
        }
    }
}
