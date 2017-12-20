namespace LendYourHome.Application.Areas.Admin.Filters
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Services;
    using Services.AdminServices;

    public class LogAttribute : ActionFilterAttribute
    {
        private readonly ILogService logs;
        private readonly UserManager<User> userManager;

        public LogAttribute(ILogService logs, UserManager<User> userManager)
        {
            this.logs = logs;
            this.userManager = userManager;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var adminId = this.userManager.GetUserId(context.HttpContext.User);
            var targetedUserName = context.HttpContext.Request.Form["UserName"].ToString();
            AdminLogType logType = AdminLogType.Ban;


            var allLogType = Enum.GetValues(typeof(AdminLogType)).Cast<AdminLogType>();

            foreach (var type in allLogType)
            {
                var typeAsString = type.ToString().ToLower();
                var methodFullNameTokens = context.ActionDescriptor.DisplayName.Split('.');
                var nameToLower = methodFullNameTokens[methodFullNameTokens.Length - 2].ToLower();

                if (nameToLower.StartsWith(typeAsString))
                {
                    logType = type;
                }
            }

            this.logs.Create(adminId, logType, targetedUserName, DateTime.UtcNow);
        }
    }
}
