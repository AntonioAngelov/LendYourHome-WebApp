namespace LendYourHome.Application.Areas.Guest.Controllers
{
    using Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(ApplicationConstants.GuestArea)]
    [Authorize]
    public abstract class GuestAreaController : Controller
    {


    }
}
