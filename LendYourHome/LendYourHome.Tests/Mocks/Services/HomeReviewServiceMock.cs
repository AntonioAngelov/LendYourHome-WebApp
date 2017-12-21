namespace LendYourHome.Tests.Mocks.Services
{
    using LendYourHome.Services;
    using Moq;

    public class HomeReviewServiceMock
    {
        public static Mock<IHomeReviewsService> New
            => new Mock<IHomeReviewsService>();
    }
}
