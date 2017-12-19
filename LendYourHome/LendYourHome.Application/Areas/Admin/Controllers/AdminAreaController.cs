namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(ApplicationConstants.AdminArea)]
    [Authorize(Roles = ApplicationConstants.AdminRole)]
    public class AdminAreaController : Controller
    {
    }
}
