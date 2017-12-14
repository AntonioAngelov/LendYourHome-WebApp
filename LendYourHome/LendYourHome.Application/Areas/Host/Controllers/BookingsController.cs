namespace LendYourHome.Application.Areas.Host.Controllers
{
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Files;

    public class BookingsController : HostAreaController
    {
        private readonly IBookingService bookings;
        private readonly IPictureService pictureService;
        private readonly UserManager<User> userManager;

        public BookingsController(IBookingService bookings, UserManager<User> userManager, IPictureService pictureService)
        {
            this.bookings = bookings;
            this.userManager = userManager;
            this.pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult Pending()
        {
            var pendingBookings = this.bookings.HostBookings(this.userManager.GetUserId(this.User), false);

            //load pictures of guests

            foreach (var booking in pendingBookings)
            {
                booking.GuestProfilePictureUrl =  this.pictureService.PreparePictureToDisplay(booking.GuestProfilePictureUrl);
            }

            return this.View(pendingBookings);
        }

        [HttpPost]
        public IActionResult Accept([FromForm] int bookingId)
        {
            var hostId = this.userManager.GetUserId(this.User);

            //check if booking with given Id and OwnerId exists

            if (!this.bookings.Exists(bookingId, hostId))
            {
                return this.NotFound();
            }

            //accept the booking
            this.bookings.Approve(bookingId);

            this.TempData[ApplicationConstants.TempDataSuccessMessageKey] = "Successfuly approved a booking request";

            return RedirectToAction("Pending", "Bookings", new {area = ApplicationConstants.HostArea});
        }

        [HttpGet]
        public IActionResult Approved()
        {
            var approvedBookings = this.bookings.HostBookings(this.userManager.GetUserId(this.User), true);

            //load pictures of guests

            foreach (var booking in approvedBookings)
            {
                booking.GuestProfilePictureUrl = this.pictureService.PreparePictureToDisplay(booking.GuestProfilePictureUrl);
            }

            return this.View(approvedBookings);
        }
    }
}
