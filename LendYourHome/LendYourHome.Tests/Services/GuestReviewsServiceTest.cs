namespace LendYourHome.Tests.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using FluentAssertions;
    using LendYourHome.Services.Implementations;
    using LendYourHome.Services.ServiceModels.Reviews;
    using Mocks;
    using Xunit;

    public class GuestReviewsServiceTest
    {
        public GuestReviewsServiceTest()
        {
            try
            {
                Tests.Initialize();
            }
            catch (Exception e)
            {
            }
        }

        [Fact]
        public async Task GetReceivedReviews_ShouldReturnOnlyTheReviewsReceivedByGuestWithGivenGuestId()
        {
            //Arrange
            var guestId = "guestId";

            var db = LendYourHomeDbMock.New();
            var guestReview1 = new GuestReview
            {
                Title = "title",
                AdditionalThoughts = "thounghts",
                SubmitDate = DateTime.MinValue,
                HostId = "hostId",
                EvaluatedGuestId = guestId
            };

            var guestReview2 = new GuestReview
            {
                Title = "title",
                AdditionalThoughts = "thounghts",
                SubmitDate = DateTime.MinValue,
                HostId = "hostId",
                EvaluatedGuestId = "someOtherID"
            };

            db.GuestReviews.Add(guestReview1);
            db.GuestReviews.Add(guestReview2);
            await db.SaveChangesAsync();

            var service = new GuestReviewsService(db);

            //Act
            var result = service.GetReceivedReviews(guestId);

            //Assert
            result
                .Count()
                .Should()
                .Be(1);
        }

        [Fact]
        public void Create_ShouldAddNewGuestReviewWithGivenParamsIntoTheDb()
        {
            //Arrange
            var hostId = "hostId";
            var guestId = "guestId";
            var evaluation = 5;
            var title = "title";
            var additionalThoughts = "somethhing";

            var db = LendYourHomeDbMock.New();
            var service = new GuestReviewsService(db);

            //Act
            service.Create(hostId, guestId, evaluation, additionalThoughts, title);

            //Assert
            db.GuestReviews
                .Any(r => r.Title == title
                          && r.EvaluatedGuestId == guestId
                          && r.HostId == hostId
                          && r.AdditionalThoughts == additionalThoughts
                          && r.Evaluation == evaluation)
                .Should()
                .BeTrue();
        }
    }
}
