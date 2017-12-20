namespace LendYourHome.Tests.Mocks.Services
{
    using LendYourHome.Services;
    using Moq;

    public class HomeServiceMock
    {
        public static Mock<IHomeService> New
            => new Mock<IHomeService>();
    }
}
