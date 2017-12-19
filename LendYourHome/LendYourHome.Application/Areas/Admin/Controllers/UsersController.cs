namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
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
        public IActionResult Active()
        {
            var activeUsers = this.users.ActiveUsers();

            return this.View(activeUsers);
        }

        [HttpPost]
        public IActionResult Ban(BanUserViewModel model)
        {
            this.users.BannUser(model.UserId, model.BanEndDate);

            TempData[ApplicationConstants.TempDataErrorMessageKey] =
                $"You banned a user!";

            return RedirectToAction("Active");
        }
    }
}
