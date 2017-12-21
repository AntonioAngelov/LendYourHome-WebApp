namespace LendYourHome.Tests.Mocks.Services
{
    using LendYourHome.Services;
    using Moq;

    public class BookingServiceMock
    {
        public static Mock<IBookingService> New
            => new Mock<IBookingService>();
    }
}
