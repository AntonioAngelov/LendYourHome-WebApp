namespace LendYourHome.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Enums;
    using Models.HomesViewModels;
    using Services;
    using Services.Files;
    using Services.ServiceModels.Homes;

    [Authorize]
    public class HomesController : Controller
    {
        private readonly IHomeService homes;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;
        private readonly IPictureService pictureService;
        private readonly IHomeReviewsService homeReviews;

        public HomesController(IHomeService homes, IUserService users, UserManager<User> userManager, IPictureService pictureService, IHomeReviewsService homeReviews)
        {
            this.homes = homes;
            this.users = users;
            this.userManager = userManager;
            this.pictureService = pictureService;
            this.homeReviews = homeReviews;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.homes.Exists(userId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (this.homes.Exists(userId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Pictures == null || model.Pictures.Count == 0)
            {
                this.ModelState.AddModelError("Pictures", "You need to add at least 1 picture of your home.");
                return this.View(model);
            }

            List<string> picturesPaths = new List<string>();

            //save pictures
            var homePicturesPath = this.GetAdequateHomePicturesPath();

            foreach (var picture in model.Pictures)
            {
                if (picture.Length > 0)
                {
                    var pictureFullPath = Path.Combine(homePicturesPath, picture.FileName);

                    using (var stream = new FileStream(pictureFullPath, FileMode.Create))
                    {
                        Task.Run(async () =>
                        {
                            await picture.CopyToAsync(stream);
                        }).Wait();

                        var pathTokens = pictureFullPath
                            .Split(new[] { "\\" }, StringSplitOptions.None);

                        var relativePicturePath = string.Join("/", pathTokens.Skip(pathTokens.Length - 2));

                        picturesPaths.Add(relativePicturePath);
                    }
                }
            }

            this.homes.Create(
                model.Country,
                model.City,
                model.Address,
                model.Sleeps.Value,
                model.Bedrooms.Value,
                model.Bathrooms.Value,
                model.PricePerNight,
                model.Additionalnformation,
                model.IsActiveOffer,
                picturesPaths,
                userId);

            this.users.AddInRole(userId, ApplicationConstants.HostRole);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search(HomesSearchingViewModel model, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            int minBedrooms = 0;
            int maxBedrooms = int.MaxValue;
            int minBathrooms = 0;
            int maxBathrooms = int.MaxValue;
            int minSleeps = 1;
            int maxSleeps = int.MaxValue;
            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;

            this.GetBedroomsRange(ref minBedrooms, ref maxBedrooms, model.Bedrooms);
            this.GetBathroomsRange(ref minBathrooms, ref maxBathrooms, model.Bathrooms);
            this.GetPricerange(ref minPrice, ref maxPrice, model.PriceRange);
            this.GetSleepsRange(ref minSleeps, ref maxSleeps, model.Sleeps);

            var homesOffers = this.homes
                .All(page,
                ApplicationConstants.HomeOffersPageListingSize,
                model.Country,
                model.City,
                minBedrooms,
                maxBedrooms,
                minBathrooms,
                maxBathrooms,
                minSleeps,
                maxSleeps,
                minPrice,
                maxPrice);

            //get adequate base64 image src

            foreach (var home in homesOffers)
            {
                home.PictureUrl = this.pictureService.PreparePictureToDisplay(home.PictureUrl);
            }

            this.ViewData[ApplicationConstants.ViewDataHomeOffersKey] = homesOffers;
            
            //set pagination data
            var query = HttpContext.Request.QueryString.Value;

            if (string.IsNullOrEmpty(query) || query.StartsWith("?page"))
            {
                query = string.Empty;
            }
            else if (query.Contains("page="))
            {
                var tokens = query.Substring(1).Split('&');
                query = string.Join("&", tokens.Take(tokens.Length - 1)).TrimStart('?');
            }
            
            model.PageListingData = new PageListingModel
            {
                Query = query,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.homes.Total(model.Country,
                                                   model.City,
                                                   minBedrooms,
                                                   maxBedrooms,
                                                   minBathrooms,
                                                   maxBathrooms,
                                                   minSleeps,
                                                   maxSleeps,
                                                   minPrice,
                                                   maxPrice) /
                                               (double)ApplicationConstants.HomeOffersPageListingSize)
            };

            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int id, int page = 1)
        {
            if (!this.homes.Exists(id))
            {
                return this.NotFound();
            }

            var homeInfo = this.homes
                .Find(id);

            //prepare owner profile picture
            homeInfo.OwnerPictureUrl = this.pictureService.PreparePictureToDisplay(homeInfo.OwnerPictureUrl);

            //load pictures
            for (int i = 0; i < homeInfo.HomesPicturesUrls.Count(); i++)
            {
                homeInfo.HomesPicturesUrls[i] = this.pictureService.PreparePictureToDisplay(homeInfo.HomesPicturesUrls[i]);
            }

            //gt reviews for home
            var reviews = this.homeReviews.GetReceivedReviews(id, page, ApplicationConstants.ReviewsPageListinSize);

            //load pictures for reviews
            foreach (var review in reviews)
            {
                review.GuestProfilePictureUrl =
                    this.pictureService.PreparePictureToDisplay(review.GuestProfilePictureUrl);
            }

            //set TempData for the booking
            this.TempData[ApplicationConstants.TempDataHomeOwnerNameKey] = homeInfo.OwnerName;

            return this.View(new HomeDetailsViewModel
            {
                HomeInfo = homeInfo,
                Reviews = reviews,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.homeReviews.TotalReceivedForHome(id) /
                                                   (double)ApplicationConstants.ReviewsPageListinSize),
                    Query = id.ToString()
                }
            });
        }

        private string GetAdequateHomePicturesPath()
        {
            var currentHomeDirectory = Guid.NewGuid();

            string path = this.pictureService.GetFilePath($"Pictures/HomesPictures/{currentHomeDirectory}");

            Directory.CreateDirectory(path);

            return path;
        }

        public void GetBathroomsRange(ref int minBathrooms, ref int maxBathrooms, NumbersRange range)
        {
            switch (range)
            {
                case NumbersRange.FromZeroToTwo:
                    maxBathrooms = 2;
                    break;
                case NumbersRange.FromThreeToFive:
                    minBathrooms = 3;
                    maxBathrooms = 5;
                    break;
                case NumbersRange.More:
                    minBathrooms = 6;
                    break;
            }
        }

        public void GetBedroomsRange(ref int minBedrooms, ref int maxBedrooms, NumbersRange range)
        {
            switch (range)
            {
                case NumbersRange.FromZeroToTwo:
                    maxBedrooms = 2;
                    break;
                case NumbersRange.FromThreeToFive:
                    minBedrooms = 3;
                    maxBedrooms = 5;
                    break;
                case NumbersRange.More:
                    minBedrooms = 6;
                    break;
            }
        }

        public void GetSleepsRange(ref int minSleeps, ref int maxSleeps, SleepsRange range)
        {
            switch (range)
            {
                case SleepsRange.FromOneToTwo:
                    maxSleeps = 2;
                    break;
                case SleepsRange.FromThreeToFive:
                    minSleeps = 3;
                    maxSleeps = 5;
                    break;
                case SleepsRange.More:
                    minSleeps = 6;
                    break;
            }
        }

        public void GetPricerange(ref decimal minPrice, ref decimal maxPrice, PriceRange range)
        {
            switch (range)
            {
                case PriceRange.FromZeroToTen:
                    maxPrice = 10;
                    break;
                case PriceRange.FromTenToTwenty:
                    minPrice = 10;
                    maxPrice = 20;
                    break;
                case PriceRange.FromTWentyToThirty:
                    minPrice = 30;
                    break;
            }
        }
    }
}