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

            if (model.Pictures == null)
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
            
            this.GetBedroomsRange(ref minBedrooms, ref maxBedrooms, model.FormSearch.Bedrooms);
            this.GetBathroomsRange(ref minBathrooms, ref maxBathrooms, model.FormSearch.Bathrooms);
            this.GetPricerange(ref minPrice, ref maxPrice, model.FormSearch.PriceRange);
            this.GetSleepsRange(ref minSleeps, ref maxSleeps, model.FormSearch.Sleeps);

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
                home.PictureUrl = this.PreparePictureToDisplay(home.PictureUrl);
            }

            return this.View(new HomesDisplayViewModel
            {
                Homes = homesOffers,
                FormSearch = model.FormSearch
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            if (!this.homes.Exists(id))
            {
                return this.NotFound();
            }

            var homeInfo = this.homes
                .Find(id);

            //prepare owner profile picture
            homeInfo.OwnerPictureUrl = this.PreparePictureToDisplay(homeInfo.OwnerPictureUrl);

            //load pictures
            for (int i = 0; i < homeInfo.HomePicturesUrls.Count(); i++)
            {
                homeInfo.HomePicturesUrls[i] = this.PreparePictureToDisplay(homeInfo.HomePicturesUrls[i]);
            }

            //set TempData for the booking
            this.TempData[ApplicationConstants.TempDataHomeOwnerNamKey] = homeInfo.OwnerName;

            return this.View(homeInfo);
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

        private string PreparePictureToDisplay(string relativePath)
        {
            var base64 = this.pictureService.GetBase64(relativePath);
            return string.Format("data:image;base64,{0}", base64); 
        }
    }
}