namespace LendYourHome.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using Common.Utilties;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.HomesViewModels;
    using Services;
    using Services.ServiceModels.Homes;

    [Authorize]
    public class HomesController : Controller
    {
        private readonly IHomeService homes;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public HomesController(IHomeService homes, IUserService users, UserManager<User> userManager)
        {
            this.homes = homes;
            this.users = users;
            this.userManager = userManager;
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

            if (model.FormSearch.MinPricePerNight > model.FormSearch.MaxPricePerNight)
            {
                ModelState.AddModelError("FormSearch.MaxPricePerNight", "The max price must be higher than the min price.");
                return this.View(new HomesDisplayViewModel
                {
                    FormSearch = model.FormSearch ?? new HomesSearchingViewModel(),
                    Homes = model.Homes ?? new List<HomeOfferServiceModel>()

                });
            }


            var homesOffers = this.homes
                .All(model.FormSearch.Country,
                model.FormSearch.City,
                model.FormSearch.Bedrooms,
                model.FormSearch.Bathrooms,
                model.FormSearch.Sleeps,
                model.FormSearch.MinPricePerNight,
                model.FormSearch.MaxPricePerNight);

            //get adequate base64 image src

            foreach (var home in homesOffers)
            {
                var relativePath = home.PictureUrl;

                var base64 = ImagePath.GetBase64(relativePath);
                home.PictureUrl = string.Format("data:image;base64,{0}", base64); ;
            }

            if (model.FormSearch.Sleeps <= 0)
            {
                model.FormSearch.Sleeps = 1;
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

            string path = FileStrem.GetFilePath($"Pictures/HomesPictures/{currentHomeDirectory}");

            Directory.CreateDirectory(path);

            return path;
        }
    }
}