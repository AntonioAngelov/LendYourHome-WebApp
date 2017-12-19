namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdminAreaController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
