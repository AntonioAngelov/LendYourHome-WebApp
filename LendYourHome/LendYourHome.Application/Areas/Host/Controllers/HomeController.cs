namespace LendYourHome.Application.Areas.Host.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Files;

    public class HomeController : HostAreaController
    {
        private readonly IHomeService homes;
        private readonly UserManager<User> userManager;
        private readonly IPictureService pictureService;

        public HomeController(IHomeService homes, UserManager<User> userManager, IPictureService pictureService)
        {
            this.homes = homes;
            this.userManager = userManager;
            this.pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Details()
        {
            var userId = userManager.GetUserId(this.User);

            var home = this.homes.Find(userId);

            for (int i = 0; i < home.HomePicturesUrls.Count; i++)
            {
                home.HomePicturesUrls[i] = this.PreparePictureToDisplay(home.HomePicturesUrls[i]);
            }

            return this.View(home);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var userId = userManager.GetUserId(this.User);
            
            var home = this.homes.Find(userId);
            var homeEditModel = new HomeEditViewModel

            {
                Additionalnformation = home.Additionalnformation,
                Address = home.Address,
                Bathrooms = home.Bathrooms,
                Bedrooms = home.Bedrooms,
                City = home.City,
                Country = home.Country,
                IsActiveOffer = home.IsActiveOffer,
                PricePerNight = home.PricePerNight,
                Sleeps = home.Sleeps
            };

            return this.View(homeEditModel);
        }

        [HttpPost]
        public IActionResult Edit(HomeEditViewModel model, List<IFormFile> pictures)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var ownerId = userManager.GetUserId(this.User);

            //edit pictures if needed 
            if (pictures != null && pictures.Count != 0)
            {
                var newPicturesPaths = new List<string>();

                var homeId = this.homes.GetId(ownerId);

                var homePicturesPath = this.pictureService.GetHomePicturesFullPath(homeId);

                //delete existing pictures
                DirectoryInfo di = new DirectoryInfo(homePicturesPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                //save new picutures
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

                            newPicturesPaths.Add(relativePicturePath);
                        }
                    }
                }

                this.pictureService.EditHomePictures(homeId, newPicturesPaths);
            }

            //edit rest of info
            this.homes.Edit(ownerId,
                model.Sleeps.Value,
                model.Country,
                model.City,
                model.Additionalnformation,
                model.Bathrooms.Value,
                model.Bedrooms.Value,
                model.IsActiveOffer,
                model.Address,
                model.PricePerNight);

            return RedirectToAction(nameof(this.Details));
        }

        private string PreparePictureToDisplay(string relativePath)
        {
            var base64 = this.pictureService.GetBase64(relativePath);
            return string.Format("data:image;base64,{0}", base64);
        }
    }
}
