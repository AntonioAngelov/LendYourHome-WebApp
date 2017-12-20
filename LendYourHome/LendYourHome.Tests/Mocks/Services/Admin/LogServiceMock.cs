namespace LendYourHome.Tests.Mocks.Services.Admin
{
    using LendYourHome.Services.AdminServices;
    using Moq;

    public class LogServiceMock
    {
        public static Mock<ILogService> New
            => new Mock<ILogService>();
    }
}
