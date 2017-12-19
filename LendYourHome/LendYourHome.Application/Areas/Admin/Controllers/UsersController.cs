namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.AdminServices;

    public class UsersController : AdminAreaController
    {
        private readonly IUserAdminService users;

        public UsersController(IUserAdminService users)
        {
            this.users = users;
        }

        [HttpGet]
        public IActionResult Active(int page = 1)
        {
            var activeUsers = this.users.ActiveUsers(page,
                AdminAreaConstants.UsersPageListingSize);

            return this.View(new ActiveUsersViewModel
            {
                ActiveUsers = activeUsers,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.users.TotalActive() /
                                                   (double)AdminAreaConstants.UsersPageListingSize)
                }

            });
        }

        [HttpGet]
        public IActionResult Banned(int page = 1)
        {
            var banedUsers = this.users.BannedUsers(page,
                AdminAreaConstants.UsersPageListingSize);

            return this.View(new BannedUsersViewModel
            {
                BannedUsers = banedUsers,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.users.TotalBanned() /
                                                   (double)AdminAreaConstants.UsersPageListingSize)
                }
            });
        }

        [HttpPost]
        public IActionResult Ban(BanUserViewModel model)
        {
            this.users.BannUser(model.UserId, model.BanEndDate);

            TempData[ApplicationConstants.TempDataErrorMessageKey] =
                $"You banned {model.UserName} till {model.BanEndDate.ToShortDateString()}!";

            return RedirectToAction("Banned");
        }

        [HttpPost]
        public IActionResult Unbann(UnbannUserViewModel model)
        {
            this.users.BannUser(model.UserId, null);

            TempData[ApplicationConstants.TempDataSuccessMessageKey] =
                $"You Unbanned {model.UserName}!";

            return RedirectToAction("Active");
        }
    }
}
