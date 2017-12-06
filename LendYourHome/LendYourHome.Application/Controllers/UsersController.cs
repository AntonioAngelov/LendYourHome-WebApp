namespace LendYourHome.Application.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.UsersViewModels;
    using Services;
    using Services.Files;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IGuestReviewsService guestReviews;
        private readonly IPictureService pictureService;

        public UsersController(IUserService users, IGuestReviewsService guestReviews, IPictureService pictureService)
        {
            this.users = users;
            this.guestReviews = guestReviews;
            this.pictureService = pictureService;
        }

        public IActionResult Details(string id)
        {
            if (!this.users.Exists(id))
            {
                return this.NotFound();
            }

            var userInfo = this.users.Details(id);
            var reveivedGuestReviews = this.guestReviews.GetReceivedReviews(id);

            var base64 = this.pictureService.GetBase64(userInfo.ProfilePictureUrl);
            this.ViewBag.imagesrc = string.Format("data:image;base64,{0}", base64);
            
            return this.View(new UserDetailsViewModel
            {
                UserInfo = userInfo,
                ReviewsReceived = reveivedGuestReviews
            });
        }
    }
}