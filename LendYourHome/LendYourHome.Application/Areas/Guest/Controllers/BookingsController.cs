namespace LendYourHome.Application.Areas.Guest.Controllers
{
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class BookingsController : GuestController
    {
        private readonly IBookingService bookings;
        private readonly IHomeService homes;
        private readonly UserManager<User> userManager;

        public BookingsController(IBookingService bookings, IHomeService homes, UserManager<User> userManager)
        {
            this.bookings = bookings;
            this.homes = homes;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (!this.homes.Exists(id))
            {
                return this.NotFound();
            }

            this.TempData[ApplicationConstants.TempDataHomeIdKey] = id;

            //if user refreshes the page the name of the owner must still show
            this.TempData.Keep(ApplicationConstants.TempDataHomeOwnerNamKey);


            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingCreateViewModel model)
        {
            if (!TempData.ContainsKey(ApplicationConstants.TempDataHomeIdKey))
            {
                return this.NotFound();
            }

            var homeId = int.Parse(this.TempData[ApplicationConstants.TempDataHomeIdKey].ToString());

            if (!this.homes.Exists(homeId))
            {
                return this.NotFound();
            }

            //if user refreshes the page the name of the owner must still show
            this.TempData.Keep(ApplicationConstants.TempDataHomeOwnerNamKey);
            this.TempData.Keep(ApplicationConstants.TempDataHomeIdKey);

            if (model.CheckOutDate <= model.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate", "Check Out Date must be after Check In Date");
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            this.bookings.Create(
                this.userManager.GetUserId(this.User),
                homeId,
                model.CheckInDate.Value,
                model.CheckOutDate.Value);

            TempData[ApplicationConstants.TempDataSuccessMessageKey] =
                $"Booking request for {TempData[ApplicationConstants.TempDataHomeOwnerNamKey]}'s home sent!";

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult Pending()
        {
            var bookings = this.bookings.GuestBookings(this.userManager.GetUserId(this.User), false);

            return this.View(bookings);
        }

        [HttpGet]
        public IActionResult Approved()
        {
            var bookings = this.bookings.GuestBookings(this.userManager.GetUserId(this.User), true);

            return this.View(bookings);
        }
    }
}
