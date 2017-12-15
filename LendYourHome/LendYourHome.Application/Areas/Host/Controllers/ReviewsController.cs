namespace LendYourHome.Application.Areas.Host.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Reviews;
    using Services;
    using Services.Files;

    public class ReviewsController : HostAreaController
    {
        private readonly IGuestReviewsService guestReviews;
        private readonly UserManager<User> userManager;
        private readonly IUserService users;
        private readonly IPictureService pictureService;

        public ReviewsController(IGuestReviewsService guestReviews, UserManager<User> userManager, IUserService users, IPictureService pictureService)
        {
            this.guestReviews = guestReviews;
            this.userManager = userManager;
            this.users = users;
            this.pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Waiting()
        {
            var hostId = this.userManager.GetUserId(this.User);

            var usersWaitingForReview = this.users.WaitingForReview(hostId);

            foreach (var users in usersWaitingForReview)
            {
                users.ProfilePictureUrl = this.pictureService.PreparePictureToDisplay(users.ProfilePictureUrl);
            }

            return this.View(usersWaitingForReview);
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var hostId = this.userManager.GetUserId(this.User);

            var guestName = this.users.GetName(id);

            this.TempData[ApplicationConstants.TempDataGuestNameKey] = guestName;

            //check if user can give review to given home
            if (!this.guestReviews.CanCreateReview(hostId, id))
            {
                this.TempData[ApplicationConstants.TempDataErrorMessageKey] = "Permission Denied";

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            this.TempData.Keep(ApplicationConstants.TempDataGuestNameKey);

            return this.View(new ReviewCreateViewModel
            {
                GuestId = id
            });
        }

        [HttpPost]
        public IActionResult Create(ReviewCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.TempData.Keep(ApplicationConstants.TempDataGuestNameKey);

                return this.View(model);
            }

            var hostId = this.userManager.GetUserId(this.User);

            if (!this.guestReviews.CanCreateReview(hostId, model.GuestId))
            {
                this.TempData[ApplicationConstants.TempDataErrorMessageKey] = "Permission Denied";

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //save review
            this.guestReviews.Create(
                hostId,
                model.GuestId,
                model.Evaluation,
                model.AdditionalThoughts,
                model.Title);

            //redirect to home with success message
            this.TempData[ApplicationConstants.TempDataSuccessMessageKey] = $"Successfully Reviewed Your Guest {this.TempData[ApplicationConstants.TempDataGuestNameKey]}";

            return RedirectToAction("Index", "Home", new { area = "" });

        }

        [HttpGet]
        public IActionResult Done(int page = 1)
        {
            var hostId = this.userManager.GetUserId(this.User);

            var reviews = this.guestReviews.Done(page,
                ApplicationConstants.DoneReviewsPageListingSize, 
                hostId);

            return this.View(new DoneGuestReviewsViewModel
            {
                Reviews = reviews,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.guestReviews.TotalDoneByUser(hostId) /
                                                   (double)ApplicationConstants.DoneReviewsPageListingSize)
                }
            });
        }

    }
}
