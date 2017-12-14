namespace LendYourHome.Application.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.UsersViewModels;
    using Services;
    using Services.Files;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IGuestReviewsService guestReviews;
        private readonly IPictureService pictureService;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, IGuestReviewsService guestReviews, IPictureService pictureService, UserManager<User> userManager)
        {
            this.users = users;
            this.guestReviews = guestReviews;
            this.pictureService = pictureService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            if (!this.users.Exists(id))
            {
                return this.NotFound();
            }

            var userInfo = this.users.Details(id);
            userInfo.ProfilePictureUrl = this.pictureService.PreparePictureToDisplay(userInfo.ProfilePictureUrl);

            var receivedGuestReviews = this.guestReviews.GetReceivedReviews(id);

            //load pictures for all reviews 
            foreach (var review in receivedGuestReviews)
            {
                review.HostProfilePictureUrl =
                    this.pictureService.PreparePictureToDisplay(review.HostProfilePictureUrl);
            }

            return this.View(new UserDetailsViewModel
            {
                UserInfo = userInfo,
                ReviewsReceived = receivedGuestReviews
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var userId = this.userManager.GetUserId(this.User);

            var userInfo = this.users.GetForEdit(userId);

            var profilePictureUrl = this.pictureService.PreparePictureToDisplay(this.users.GetUserProfilePicture(userId));

            return this.View(new UserEditViewModel
            {
                FormDataModel = userInfo,
                ProfilePictureUrl = profilePictureUrl
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(UserEditViewModel model, IFormFile picture)
        {
            var userId = this.userManager.GetUserId(this.User);

            var currentPhotoUrl = this.users.GetUserProfilePicture(userId);

            if (this.users.UsernameIsTaken(userId, model.FormDataModel.UserName))
            {
                ModelState.AddModelError("FormDataModel.UserName", "Username already taken.");
            }

            if (!ModelState.IsValid)
            {
                return this.View(new UserEditViewModel
                {
                    FormDataModel = model.FormDataModel,
                    ProfilePictureUrl = this.pictureService.PreparePictureToDisplay(currentPhotoUrl)
                });
            }

            string newPictureUrl = null;
            
            if (picture != null)
            {
                var fullPath = string.Empty;

                if (currentPhotoUrl.Split('/').Length == 3)
                {
                    fullPath = this.GetAdequateProfilePicturePath();
                }
                else
                {
                    fullPath = this.pictureService.GetUserProfilePictureFullPath(userId);

                    //delete existing picture
                    DirectoryInfo di = new DirectoryInfo(fullPath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                }

                if (picture.Length > 0)
                {
                    var pictureFullPath = Path.Combine(fullPath, picture.FileName);

                    using (var stream = new FileStream(pictureFullPath, FileMode.Create))
                    {
                        Task.Run(async () =>
                        {
                            await picture.CopyToAsync(stream);
                        }).Wait();

                        var pathTokens = pictureFullPath
                            .Split(new[] { "\\" }, StringSplitOptions.None);

                        var relativePicturePath = string.Join("/", pathTokens.Skip(pathTokens.Length - 2));

                        newPictureUrl = relativePicturePath;
                    }
                }
            }

            this.users.Edit(userId,
                model.FormDataModel.UserName,
                model.FormDataModel.AdditionalInformation,
                model.FormDataModel.Address,
                newPictureUrl);

            return this.RedirectToAction("Index", "Home", new {area = ""});
        }

        private string GetAdequateProfilePicturePath()
        {
            var currentPictureDirectory = Guid.NewGuid();

            string path = this.pictureService.GetFilePath($"Pictures/ProfilePictures/{currentPictureDirectory}");

            Directory.CreateDirectory(path);

            return path;
        }
    }
}