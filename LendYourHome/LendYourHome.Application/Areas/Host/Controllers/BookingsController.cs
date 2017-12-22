namespace LendYourHome.Application.Areas.Host.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Bookings;
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
        public IActionResult Pending(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var pendingBookings = this.bookings.HostBookings(page,
                ApplicationConstants.HomeBookingPageListingSize,
                userId, false);

            //load pictures of guests

            foreach (var booking in pendingBookings)
            {
                booking.GuestProfilePictureUrl =  this.pictureService.PreparePictureToDisplay(booking.GuestProfilePictureUrl);
            }

            return this.View(new HostBookingsViewModel
            {
                Bookings = pendingBookings,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.bookings.TotalBookingsByHost(userId, false) /
                                                   (double)ApplicationConstants.HomeBookingPageListingSize)
                }
            });
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
        public IActionResult Approved(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var approvedBookings = this.bookings.HostBookings(page,
                ApplicationConstants.HomeBookingPageListingSize, userId, true);

            //load pictures of guests

            foreach (var booking in approvedBookings)
            {
                booking.GuestProfilePictureUrl = this.pictureService.PreparePictureToDisplay(booking.GuestProfilePictureUrl);
            }

            return this.View(new HostBookingsViewModel
            {
                Bookings = approvedBookings,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.bookings.TotalBookingsByHost(userId, true) /
                                                   (double)ApplicationConstants.HomeBookingPageListingSize)
                }
            });
        }
    }
}
