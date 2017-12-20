namespace LendYourHome.Tests.Application.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using LendYourHome.Application.Areas.Admin.Controllers;
    using LendYourHome.Application.Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;
    using Mocks.Services.Admin;
    using Moq;
    using Services.AdminServices.AdminServiceModels;
    using Xunit;

    public class LogsControllerTest
    {
        [Fact]
        public void All_ShouldReturnViewResultWithCorrectModel()
        {
            //Arrange
            var expectedLogs = new List<LogAdminServiceModel>
            {
                new LogAdminServiceModel()
                {
                    AdminName = "Admin",
                    LogType = 0,
                    SubmitDate = DateTime.UtcNow,
                    TargetedUserName = "Pesho"
                }
            };

            var logService = LogServiceMock.New;
            logService
                .Setup(ls => ls.All(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedLogs);

            var controller = new LogsController(logService.Object);

            //Act
            var result = controller.All(It.IsAny<int>());

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<LogsListingViewModel>().Logs.Equals(expectedLogs));
        }
    }
}
