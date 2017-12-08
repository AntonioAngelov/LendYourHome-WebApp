namespace LendYourHome.Application.Areas.Host.Controllers
{
    using Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(ApplicationConstants.HostArea)]
    [Authorize(Roles = ApplicationConstants.HostRole)]
    public class HostAreaController : Controller
    {
    }
}
