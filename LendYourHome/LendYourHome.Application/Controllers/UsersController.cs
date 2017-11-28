namespace LendYourHome.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.UsersViewModels;
    using Services;
    
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IGuestReviewsService guestReviews;

        public UsersController(IUserService users, IGuestReviewsService guestReviews)
        {
            this.users = users;
            this.guestReviews = guestReviews;
        }

        public IActionResult Details(string id)
        {
            if (!this.users.Exists(id))
            {
                return this.NotFound();
            }

            var userInfo = this.users.Details(id);
            var reveivedGuestReviews = this.guestReviews.GetReceivedReviews(id);

            return this.View(new UserDetailsViewModel
            {
                UserInfo = userInfo,
                ReviewsReceived = reveivedGuestReviews
            });
        }
    }
}