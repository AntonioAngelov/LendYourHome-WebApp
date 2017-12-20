namespace LendYourHome.Tests.Application.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using LendYourHome.Application.Areas.Admin.Controllers;
    using LendYourHome.Application.Areas.Admin.Models.Users;
    using Microsoft.AspNetCore.Mvc;
    using Mocks.Services;
    using Mocks.Services.Admin;
    using Moq;
    using Services.AdminServices.AdminServiceModels;
    using Xunit;

    public class UsersControllerTest
    {
        [Fact]
        public void Active_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange
            var expectedUsers = new List<ActiveUserAdminServiceModel>
            {
                new ActiveUserAdminServiceModel
                {
                    Address = "address",
                    Email = "email@email.com",
                    Id = "id",
                    IsHost = "Yes",
                    UserName = "GOsho"
                }
            };

            var userService = UserAdminServiceMock.New;
            userService
                .Setup(us => us.ActiveUsers(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedUsers);

            var controller = new UsersController(userService.Object);

            //Act
            var result = controller.Active(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<ActiveUsersViewModel>().ActiveUsers.Equals(expectedUsers));
        }

        [Fact]
        public void Banned_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange
            var expectedUsers = new List<BannedUserAdminServiceModel>
            {
                new BannedUserAdminServiceModel
                {
                    Address = "address",
                    Email = "email@email.com",
                    Id = "id",
                    UserName = "GOsho",
                    BanEndDate = DateTime.MaxValue
                }
            };

            var userService = UserAdminServiceMock.New;
            userService
                .Setup(us => us.BannedUsers(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedUsers);

            var controller = new UsersController(userService.Object);

            //Act
            var result = controller.Banned(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<BannedUsersViewModel>().BannedUsers.Equals(expectedUsers));
        }
    }
}
