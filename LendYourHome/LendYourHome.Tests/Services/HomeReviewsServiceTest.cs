namespace LendYourHome.Tests.Services
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using LendYourHome.Services.Implementations;
    using Mocks;
    using Xunit;

    public class HomeReviewsServiceTest
    {
        public HomeReviewsServiceTest()
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
        public void Create_ShouldAddNewReviewWithGivenParamsIntoTheDb()
        {
            //Arrange
            var userId = "user";
            var homeId = 1;
            var evaluation = 5;
            var additionalTh = "something";
            var title = "good";

            var db = LendYourHomeDbMock.New();
            var service = new HomeReviewsService(db);

            //Act
            service.Create(userId, homeId, evaluation, additionalTh, title);

            //Assert
            db.HomeReviews
                .Any(r => r.HomeId == homeId
                          && r.EvaluatingGuestId == userId
                          && r.Title == title
                          && r.Evaluation == evaluation
                          && r.AdditionalThoughts == additionalTh)
                .Should()
                .BeTrue();
        }
    }
}
