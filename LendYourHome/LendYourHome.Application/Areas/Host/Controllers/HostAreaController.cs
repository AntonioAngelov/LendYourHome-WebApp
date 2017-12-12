namespace LendYourHome.Application.Areas.Host.Controllers
{
    using Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(ApplicationConstants.HostArea)]
    [Authorize(Policy = ApplicationConstants.HostEntryPolicy)]
    public class HostAreaController : Controller
    {
    }
}
