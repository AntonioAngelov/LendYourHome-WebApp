namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
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
        [ServiceFilter(typeof(LogAttribute))]
        public IActionResult Ban(BanUserViewModel model)
        {
            this.users.BannUser(model.UserId, model.BanEndDate);

            TempData[ApplicationConstants.TempDataErrorMessageKey] =
                $"You banned {model.UserName} till {model.BanEndDate.ToShortDateString()}!";

            return RedirectToAction("Banned");
        }

        [HttpPost]
        [ServiceFilter(typeof(LogAttribute))]
        public IActionResult Unban(UserFormModel model)
        {
            this.users.BannUser(model.UserId, null);

            TempData[ApplicationConstants.TempDataSuccessMessageKey] =
                $"You Unbanned {model.UserName}!";

            return RedirectToAction("Active");
        }

        [HttpPost]
        [ServiceFilter(typeof(LogAttribute))]
        public IActionResult MakeAdmin(UserFormModel model)
        {
            this.users.MakeAdmin(model.UserId);

            TempData[ApplicationConstants.TempDataSuccessMessageKey] =
                $"You made {model.UserName} an Admin!";

            return RedirectToAction("Active");
        }
    }
}
