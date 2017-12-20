namespace LendYourHome.Application.Areas.Admin.Controllers
{
    using System;
    using Application.Models;
    using Common.Constants;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.AdminServices;

    public class LogsController : AdminAreaController
    {
        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        [Route("admin/logs")]
        public IActionResult All(int page = 1)
        {
            var neededLogs = this.logs.All(page,
                AdminAreaConstants.LogsPageListingSize);

            return this.View(new LogsListingViewModel
            {
                Logs = neededLogs,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int) Math.Ceiling(this.logs.Total() /
                                                    (double) AdminAreaConstants.LogsPageListingSize)
                }
            });
        }
    }
}
