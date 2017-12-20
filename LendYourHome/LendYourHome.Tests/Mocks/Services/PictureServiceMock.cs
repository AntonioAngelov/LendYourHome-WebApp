namespace LendYourHome.Tests.Mocks.Services
{
    using Data;
    using LendYourHome.Services.Files;
    using Moq;

    public class PictureServiceMock
    {
        public static Mock<IPictureService> New
            => new Mock<IPictureService>();
    }
}
