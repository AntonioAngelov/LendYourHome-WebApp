namespace LendYourHome.Application.Controllers
{
    using System;
    using System.IO;
    using Common.Utilties;
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

            string path = FileStrem.GetFilePath(userInfo.ProfilePictureUrl);
            byte[] imagebyte = LoadImage.GetPictureData(path);
            var base64 = Convert.ToBase64String(imagebyte);
            this.ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);


            //var a = Directory.Exists(FileStrem.GetFilePath("Pictures/ProfilePictures"));
            //var b = Directory.Exists(FileStrem.GetFilePath("wwwroot/Pictures/ProfilePictures/neshtosi"));

            
            return this.View(new UserDetailsViewModel
            {
                UserInfo = userInfo,
                ReviewsReceived = reveivedGuestReviews
            });
        }
    }
}