namespace LendYourHome.Application.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Files;

    public class HomeController : Controller
    {
        private readonly IHomeService homes;
        private readonly IUserService users;
        private readonly IPictureService pictureService;

        public HomeController(IHomeService homes, IUserService users, IPictureService pictureService)
        {
            this.homes = homes;
            this.users = users;
            this.pictureService = pictureService;
        }

        public IActionResult Index()
        {
            var topSixHomes = this.homes.TopSixByAverageRating();

            //load homes pictures
            foreach (var home in topSixHomes)
            {
                home.PictureUrl = this.pictureService.PreparePictureToDisplay(home.PictureUrl);
            }
             
            var topSixGuests = this.users.TopSixGuestsByAverageRating();

            foreach (var guest in topSixGuests)
            {
                guest.ProfilePictureUrl = this.pictureService.PreparePictureToDisplay(guest.ProfilePictureUrl);
            }

            return View(new HomeIndexViewModel
            {
                TopHomes = topSixHomes,
                TopGuests = topSixGuests
            });
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
