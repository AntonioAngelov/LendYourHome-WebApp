namespace LendYourHome.Application.Areas.Guest.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Reviews;
    using Services;
    using Services.Files;

    public class ReviewsController : GuestAreaController
    {
        private readonly IHomeReviewsService homeReviews;
        private readonly IHomeService homes;
        private readonly UserManager<User> userManager;
        private readonly IPictureService pictureService;

        public ReviewsController(IHomeReviewsService homeReviews, IHomeService homes, UserManager<User> userManager, IPictureService pictureService)
        {
            this.homeReviews = homeReviews;
            this.homes = homes;
            this.userManager = userManager;
            this.pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Waiting()
        {
            var userId = this.userManager.GetUserId(this.User);

            var homesWaitingForReview = this.homes.WaitingForReview(userId);

            foreach (var home in homesWaitingForReview)
            {
                home.OwnerPictureUrl = this.pictureService.PreparePictureToDisplay(home.OwnerPictureUrl);
            }

            return this.View(homesWaitingForReview);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var ownerName = this.homes.GetOwnerName(id);

            this.TempData[ApplicationConstants.TempDataHomeOwnerNameKey] = ownerName;

            //check if user can give review to given home
            if (!this.homeReviews.CanCreateReview(userId, id))
            {
                this.TempData[ApplicationConstants.TempDataErrorMessageKey] = "Permission Denied";

                return RedirectToAction("Index", "Home", new {area = ""});
            }

            this.TempData.Keep(ApplicationConstants.TempDataHomeOwnerNameKey);
 
            return this.View(new ReviewCreateViewModel
            {
                HomeId = id
            });
        }

        [HttpPost]
        public IActionResult Create(ReviewCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.TempData.Keep(ApplicationConstants.TempDataHomeOwnerNameKey);

                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            if (!this.homeReviews.CanCreateReview(userId, model.HomeId))
            {
                this.TempData[ApplicationConstants.TempDataErrorMessageKey] = "Permission Denied";

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //save review
            this.homeReviews.Create(
                userId,
                model.HomeId,
                model.Evaluation,
                model.AdditionalThoughts,
                model.Title);
            
            //redirect to home with success message
            this.TempData[ApplicationConstants.TempDataSuccessMessageKey] = $"Successfully Reviewed {this.TempData[ApplicationConstants.TempDataHomeOwnerNameKey]}'s home";

            return RedirectToAction(nameof(this.Done));

        }

        [HttpGet]
        public IActionResult Done(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var reviews = this.homeReviews.Done(page,
                ApplicationConstants.DoneReviewsPageListingSize,
                userId);

            return this.View(new DoneHomeReviewsViewModel
            {
                Reviews = reviews,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.homeReviews.TotalDoneByUser(userId) /
                                                   (double)ApplicationConstants.DoneReviewsPageListingSize)
                }
            });
        }
    }
}
