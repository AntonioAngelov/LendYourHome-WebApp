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

        public HomesController(IHomeService homes, IUserService users, UserManager<User> userManager, IPictureService pictureService)
        {
            this.homes = homes;
            this.users = users;
            this.userManager = userManager;
            this.pictureService = pictureService;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeCreateViewModel model, List<IFormFile> pictures)
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

            List<string> picturesPaths = new List<string>();

            //save pictures
            var homePicturesPath = this.GetAdequateHomePictersPath();

            foreach (var picture in pictures)
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
        public IActionResult Search(HomesDisplayViewModel model)
        {
            //if client acces the page for a first time only the form must be displayed
            if (model.FormSearch == null && model.Homes == null)
            {
                return this.View(new HomesDisplayViewModel()
                {
                    Homes = new List<HomeOfferServiceModel>()
                });
            }

            if (!ModelState.IsValid)
            {
                return this.View(new HomesDisplayViewModel
                {
                    FormSearch = model.FormSearch ?? new HomesSearchingViewModel(),
                    Homes = model.Homes ?? new List<HomeOfferServiceModel>()

                });
            }

            int minBedrooms = 0;
            int maxBedrooms = int.MaxValue;
            int minBathrooms = 0;
            int maxBathrooms = int.MaxValue;
            int minSleeps = 1;
            int maxSleeps = int.MaxValue;
            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;

            switch (model.FormSearch.Bathrooms)
            {
                case Models.Enums.NumbersRange.FromZeroToTwo:
                    maxBathrooms = 2;
                    break;
                case Models.Enums.NumbersRange.FromThreeToFive:
                    minBathrooms = 3;
                    maxBathrooms = 5;
                    break;
                case Models.Enums.NumbersRange.More:
                    minBathrooms = 6;
                    break;
            }

            switch (model.FormSearch.Bedrooms)
            {
                case Models.Enums.NumbersRange.FromZeroToTwo:
                    maxBedrooms = 2;
                    break;
                case Models.Enums.NumbersRange.FromThreeToFive:
                    minBedrooms = 3;
                    maxBedrooms = 5;
                    break;
                case Models.Enums.NumbersRange.More:
                    minBedrooms = 6;
                    break;
            }

            switch (model.FormSearch.PriceRange)
            {
                case Models.Enums.PriceRange.FromZeroToTen:
                    maxPrice = 10;
                    break;
                case Models.Enums.PriceRange.FromTenToTwenty:
                    minPrice = 10;
                    maxPrice = 20;
                    break;
                case Models.Enums.PriceRange.FromTWentyToThirty:
                    minPrice = 30;
                    break;
            }

            switch (model.FormSearch.Sleeps)
            {
                case Models.Enums.SleepsRange.FromOneToTwo:
                    maxSleeps = 2;
                    break;
                case Models.Enums.SleepsRange.FromThreeToFive:
                    minSleeps = 3;
                    maxSleeps = 5;
                    break;
                case Models.Enums.SleepsRange.More:
                    minSleeps = 6;
                    break;
            }

            var homesOffers = this.homes
                .All(model.FormSearch.Country,
                model.FormSearch.City,
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
                var relativePath = home.PictureUrl;

                var base64 = this.pictureService.GetBase64(relativePath);
                home.PictureUrl = string.Format("data:image;base64,{0}", base64); ;
            }
            
            return this.View(new HomesDisplayViewModel
            {
                Homes = homesOffers,
                FormSearch = model.FormSearch
            });
        }

        private string GetAdequateHomePictersPath()
        {
            var currentHomeDirectory = Guid.NewGuid(); 

            string path = this.pictureService.GetFilePath($"Pictures/HomesPictures/{currentHomeDirectory}");

            Directory.CreateDirectory(path);

            return path;
        }
    }
}