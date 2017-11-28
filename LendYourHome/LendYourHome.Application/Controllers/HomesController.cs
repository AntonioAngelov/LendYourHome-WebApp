namespace LendYourHome.Application.Controllers
{
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.HomesViewModels;
    using Services;

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
                userId);

            this.users.AddInRole(userId, ApplicationConstants.HostRole);

            return RedirectToAction("Index", "Home");
        }
    }
}