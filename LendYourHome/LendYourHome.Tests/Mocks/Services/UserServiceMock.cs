namespace LendYourHome.Tests.Mocks.Services
{
    using LendYourHome.Services;
    using Moq;

    public class UserServiceMock
    {
        public static Mock<IUserService> New
            => new Mock<IUserService>();
    }
}
